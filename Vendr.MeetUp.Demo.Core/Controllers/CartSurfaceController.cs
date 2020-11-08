using System.Linq;
using System.Web.Mvc;
using Umbraco.Core.Logging;
using Umbraco.Web.Mvc;
using Vendr.Core;
using Vendr.Core.Exceptions;
using Vendr.Core.Services;
using Vendr.Core.Session;
using Vendr.MeetUp.Demo.Core.Extensions;
using Vendr.MeetUp.Demo.Core.Models.ViewModels;

namespace Vendr.MeetUp.Demo.Core.Controllers
{
    public class CartSurfaceController : SurfaceController
    {
        //collection of methods to interact with a Vendr session - order, high level details like tax, shipping country etc
        private readonly ISessionManager _sessionManager;

        //Vendr Order Service
        private readonly IOrderService _orderService;

        private readonly IUnitOfWorkProvider _unitOfWorkProvider;

        private readonly ILogger _logger;

        public CartSurfaceController(ISessionManager sessionManager, IOrderService orderService, IUnitOfWorkProvider unitOfWorkProvider, ILogger logger)
        {
            _sessionManager = sessionManager;
            _orderService = orderService;
            _unitOfWorkProvider = unitOfWorkProvider;
            _logger = logger;
        }
        
        [ChildActionOnly]
        public ActionResult CartCount()
        {
            var store = CurrentPage.GetStore();
            var currentOrder = _sessionManager.GetCurrentOrder(store.Id);

            return this.PartialView("Shop/CartCount", currentOrder?.TotalQuantity ?? 0);
        }

        
        public ActionResult AddToCart(AddToCartViewModel addToCartViewModel)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get the current store
                    var store = CurrentPage.GetStore();

                    //get the product as IProductSnapshot
                    var product = Umbraco.Content(addToCartViewModel.ProductReference).AsVendrProduct();

                    //gets the writable order entity and add product to it
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow);

                     order.AddProduct(addToCartViewModel.ProductReference.ToString(), addToCartViewModel.Quantity);

                    //save the order
                    _orderService.SaveOrder(order);

                    //saving product name for notification
                    TempData["productName"] = product.Name;
                    TempData["productAdded"] = true;
                    TempData["productReference"] = addToCartViewModel.ProductReference;
                    
                    //complete the unit of work
                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("productReference", "Failed to add product to cart");
                _logger.Error<CartSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }


            return RedirectToCurrentUmbracoPage();
        }
        
        [HttpPost]
        public ActionResult UpdateCart(UpdateCartViewModel updateCart)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get the current store
                    var store = CurrentPage.GetStore();

                    //gets the writable order entity
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow);

                    foreach (var lineItem in updateCart.OrderLines)
                    {
                        //set the quantity of the lineitem
                        order.WithOrderLine(lineItem.LineItemId).SetQuantity(lineItem.Quantity);
                    }

                    //save the order
                    _orderService.SaveOrder(order);

                    //saving product name for notification
                    TempData["cartUpdated"] = "true";

                    //complete the unit of work
                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("productReference", "Failed to update cart");
                _logger.Error<CartSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }

        public ActionResult EmptyCart()
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get the current store
                    var store = CurrentPage.GetStore();

                    //gets the writable order entity
                    var order = _sessionManager.GetCurrentOrder(store.Id)
                        .AsWritable(uow);

                    foreach (var lineItem in order.OrderLines)
                    {
                        order.RemoveOrderLine(lineItem.Id);
                    }

                    //save the order
                    _orderService.SaveOrder(order);

                    //saving product name for notification
                    TempData["cartEmpty"] = "true";

                    //complete the unit of work
                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("productReference", "Failed to update cart");
                _logger.Error<CartSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }

        public ActionResult RemoveFromCart(RemoveFromCartViewModel lineItem)
        {
            try
            {
                using (var uow = _unitOfWorkProvider.Create())
                {
                    //get the current store
                    var store = CurrentPage.GetStore();

                    //gets the writable order entity and remove the orderline with the id from the model
                    var order = _sessionManager.GetOrCreateCurrentOrder(store.Id)
                        .AsWritable(uow)
                        .RemoveOrderLine(lineItem.OrderLineId);

                    //save the order
                    _orderService.SaveOrder(order);

                    //saving product name for notification
                    TempData["itemRemoved"] = "true";
                    TempData["itemName"] = lineItem.ProductName;

                    //complete the unit of work
                    uow.Complete();
                }
            }
            catch (ValidationException ex)
            {
                ModelState.AddModelError("productReference", "Failed to remove item from cart");
                _logger.Error<CartSurfaceController>(ex.Message);
                return CurrentUmbracoPage();
            }

            return RedirectToCurrentUmbracoPage();
        }
    }
}