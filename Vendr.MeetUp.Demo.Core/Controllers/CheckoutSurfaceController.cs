using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using Vendr.Core;
using Vendr.Core.Services;
using Vendr.Core.Session;
using Vendr.MeetUp.Demo.CMSModels;
using Vendr.MeetUp.Demo.Core.Extensions;
using Vendr.MeetUp.Demo.Core.Models.ViewModels;

namespace Vendr.MeetUp.Demo.Core.Controllers
{
    public class CheckoutSurfaceController : SurfaceController
    {
        private readonly ISessionManager _sessionManager;

        private readonly IOrderService _orderService;

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;

        private readonly ILogger _logger;

        private readonly IMemberService _memberService;

        public CheckoutSurfaceController(ISessionManager sessionManager, IOrderService orderService,
            IUnitOfWorkProvider unitOfWorkProvider, ILogger logger, IMemberService memberService)
        {
            _sessionManager = sessionManager;
            _orderService = orderService;
            _unitOfWorkProvider = unitOfWorkProvider;
            _logger = logger;
            _memberService = memberService;
        }

        public ActionResult ApplyDiscount(string discountCode)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    var store = CurrentPage.GetStore();
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .Redeem(discountCode);

                    _orderService.SaveOrder(order);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to remove discount code");
                _logger.Error<CheckoutSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }
        
        public ActionResult RemoveDiscount(string discountCode)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    var store = CurrentPage.GetStore();
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .Unredeem(discountCode);

                    _orderService.SaveOrder(order);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to remove discount code");
                _logger.Error<CheckoutSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }

       [HttpPost]
        public ActionResult UpdateOrderCustomerInformation(UpdateCustomerInformationViewModel model)
        {
            try
            {
                //create unit of work 
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get current store
                    var store = CurrentPage.GetStore();
                    
                    //get current order as a writable entity and set the property values from the viewmodel
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .SetProperties(new Dictionary<string, string>
                        {
                            {Constants.Properties.Customer.EmailPropertyAlias, model.Email},
                            {"marketingOptIn", model.MarketingOptIn ? "1" : "0"},

                            {Constants.Properties.Customer.FirstNamePropertyAlias, model.BillingAddress.FirstName},
                            {Constants.Properties.Customer.LastNamePropertyAlias, model.BillingAddress.LastName},
                            {"billingAddressLine1", model.BillingAddress.Line1},
                            {"billingAddressLine2", model.BillingAddress.Line2},
                            {"billingCity", model.BillingAddress.City},
                            {"billingZipCode", model.BillingAddress.ZipCode},
                            {"billingTelephone", model.BillingAddress.Telephone},
                            {"shippingSameAsBilling", model.ShippingSameAsBilling ? "1" : "0"},
                            {
                                "shippingFirstName",
                                model.ShippingSameAsBilling
                                    ? model.BillingAddress.FirstName
                                    : model.ShippingAddress.FirstName
                            },
                            {
                                "shippingLastName",
                                model.ShippingSameAsBilling
                                    ? model.BillingAddress.LastName
                                    : model.ShippingAddress.LastName
                            },
                            {
                                "shippingAddressLine1",
                                model.ShippingSameAsBilling ? model.BillingAddress.Line1 : model.ShippingAddress.Line1
                            },
                            {
                                "shippingAddressLine2",
                                model.ShippingSameAsBilling ? model.BillingAddress.Line2 : model.ShippingAddress.Line2
                            },
                            {
                                "shippingCity",
                                model.ShippingSameAsBilling ? model.BillingAddress.City : model.ShippingAddress.City
                            },
                            {
                                "shippingZipCode",
                                model.ShippingSameAsBilling
                                    ? model.BillingAddress.ZipCode
                                    : model.ShippingAddress.ZipCode
                            },
                            {
                                "shippingTelephone",
                                model.ShippingSameAsBilling
                                    ? model.BillingAddress.Telephone
                                    : model.ShippingAddress.Telephone
                            },

                            {"comments", model.Comments},
                            {"giftWrap", model.GiftWrap? "1" : "0"},
                            {"giftMessage", model.GiftMessage}
                        })
                        .SetPaymentCountryRegion(model.BillingAddress.Country, null) //set the payment country of the order
                        .SetShippingCountryRegion(
                            (model.ShippingSameAsBilling ? model.BillingAddress.Country : model.ShippingAddress.Country),
                            null); //set shipping country of the order

                    if (model.GiftWrap)
                    {
                        //gift wrap service, added as a zero value product
                        var giftWrapProduct = CurrentPage.GetHome().FirstChild<Products>().Children<Product>()
                            .FirstOrDefault(p => p.ProductServices).AsVendrProduct();

                        //add to order 
                        order.AddProduct(giftWrapProduct.ProductReference.ToString(), 1);
                    }

                    //save to order
                    _orderService.SaveOrder(order);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to update information");
                _logger.Error<CheckoutSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            if (model.NextStep.HasValue)
                return RedirectToUmbracoPage(model.NextStep.Value);

            return RedirectToCurrentUmbracoPage();
        }

        public ActionResult UpdateOrderShippingMethod(UpdateOrderShippingMethodViewModel model)
        {
            try
            {
                //create a unit of work
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get the current store 
                    var store = CurrentPage.GetStore();
                    
                    // get the order as a writable entity and set the shipping method
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .SetShippingMethod(model.ShippingMethod);

                    //save the order
                    _orderService.SaveOrder(order);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to set order shipping method");
                _logger.Error<CheckoutSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            if (model.NextStep.HasValue)
                return RedirectToUmbracoPage(model.NextStep.Value);

            return RedirectToCurrentUmbracoPage();
        }

        public ActionResult UpdateOrderPaymentMethod(UpdateOrderPaymentMethodViewModel model)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    var store = CurrentPage.GetStore();
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .SetPaymentMethod(model.PaymentMethod);

                    _orderService.SaveOrder(order);

                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("", "Failed to set order payment method");
                _logger.Error<CheckoutSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            if (model.NextStep.HasValue)
                return RedirectToUmbracoPage(model.NextStep.Value);

            return RedirectToCurrentUmbracoPage();
        }
    }
}