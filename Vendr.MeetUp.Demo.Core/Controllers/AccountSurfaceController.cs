using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Vendr.Core;
using Vendr.Core.Models;
using Vendr.Core.Services;
using Vendr.MeetUp.Demo.CMSModels;
using Vendr.MeetUp.Demo.Core.Extensions;
using Vendr.MeetUp.Demo.Core.Models.ViewModels;

namespace Vendr.MeetUp.Demo.Core.Controllers
{
    public class AccountSurfaceController : SurfaceController
    {
        private readonly IMemberService _memberService;

        private readonly IOrderService _orderService;
        
        private readonly IUnitOfWorkProvider _unitOfWorkProvider;

        public AccountSurfaceController(IMemberService memberService, IOrderService orderService, IUnitOfWorkProvider unitOfWorkProvider)
        {
            _memberService = memberService;
            _orderService = orderService;
            _unitOfWorkProvider = unitOfWorkProvider;
        }

        // GET
        public ActionResult RegisterUser(RegisterViewModel model)
        {
            var member = _memberService.CreateMember(model.Email, model.Email, model.FirstName + " " + model.LastName,
                Member.ModelTypeAlias);
            member.SetValue("addressLine1", model.AddressLine1);
            member.SetValue("addressLine2", model.AddressLine2);
            member.SetValue("city", model.City);
            member.SetValue("country", model.Country);
            member.SetValue("postCode", model.PostCode);
            member.SetValue("telephone", model.Telephone);
            _memberService.Save(member);
            _memberService.SavePassword(member, model.Password);
            _memberService.AssignRoles(new string[] {member.Email}, new string[] {"Shop Customer"});

            Login(model.Email,model.Password);

            var checkoutPage = Umbraco.ContentSingleAtXPath("//home/checkout");
            return RedirectToUmbracoPage(checkoutPage);
        }

        public ActionResult Logout()
        {
            Members.Logout();
            FormsAuthentication.SignOut();
            return RedirectToCurrentUmbracoPage();
        }
        
        public ActionResult Login(LoginViewModel model)
        {
            Login(model.Username, model.Password);
            return RedirectToUmbracoPage(Umbraco.ContentSingleAtXPath("//home/account"));
        }
        
        public ActionResult GetMemberOrders(LoginViewModel model)
        {
            IEnumerable<OrderReadOnly> orders = null;
           var loggedInMember = Members.GetByUsername(Members.CurrentUserName);
           if (loggedInMember != null)
           { 
               orders =
                   _orderService.GetFinalizedOrdersForCustomer(CurrentPage.GetStore().Id,
                       loggedInMember.Key.ToString());
           }

           return this.PartialView("Shop/MemberOrders", orders);
        }
        
        [HttpPost]
        public ActionResult LinkOrder(string orderNumber)
        {
            var loggedInMember = Members.GetByUsername(Members.CurrentUserName);

            if (loggedInMember == null) return RedirectToCurrentUmbracoPage();
            using (var uow = _unitOfWorkProvider.Create())
            {
                //gets the writable order entity and add product to it
                var order = _orderService.GetOrder(CurrentPage.GetStore().Id, orderNumber)
                    .AsWritable(uow)
                    .AssignToCustomer(loggedInMember.Key.ToString());

                //save the order
                _orderService.SaveOrder(order);

                //saving product name for notification
                TempData["orderLinked"] = "true";

                //complete the unit of work
                uow.Complete();
            }

            return RedirectToCurrentUmbracoPage();
        }

      private void Login(string userName, string password)
        {
            Members.Login(userName, password);
            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket
            (1,
                userName,
                DateTime.Now,
                DateTime.Now.AddDays(42),
                true,
                "Autologon");

            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            authCookie.Expires = authTicket.Expiration;
            Response.Cookies.Add(authCookie);
        }
    }
}