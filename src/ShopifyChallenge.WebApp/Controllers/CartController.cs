using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Sales.Application.Events;
using ShopifyChallenge.Sales.Application.Services;
using ShopifyChallenge.Sales.Application.ViewModels;
using System;
using System.Threading.Tasks;

namespace ShopifyChallenge.WebApp.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications,
                                  IProductService productService,
                                  IMediatorHandler mediatorHandler,
                                  IOrderService orderService) : base(notifications, mediatorHandler)
        {
            _productService = productService;
            _mediatorHandler = mediatorHandler;
            _orderService = orderService;
        }

        [Route("my-cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("my-cart")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            if (product.InventoryQuantity < quantity)
            {
                TempData["Error"] = "Product with insufficient inventory quantity";
                return RedirectToAction("ProductDetail", "Display", new { id });
            }

            var evnt = new AddOrderLineEvent(CustomerId, Guid.Empty, product.Id, product.Name, product.Price, quantity);
            await _mediatorHandler.PublishEvent(evnt);

            if (IsOperationValid())
            {
                return RedirectToAction("Index");
            }

            TempData["Error"] = GetErrorMessages();
            return RedirectToAction("ProductDetail", "Display", new { id });
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoverItem(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            var evnt = new RemoveOrderLineEvent(CustomerId, Guid.Empty, id);
            await _mediatorHandler.PublishEvent(evnt);

            if (IsOperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderService.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            var evnt = new UpdateOrderLineEvent(CustomerId, id, quantity);
            await _mediatorHandler.PublishEvent(evnt);

            if (IsOperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderService.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("apply-coupon")]
        public async Task<IActionResult> ApplyCoupon(string couponCode)
        {
            var evnt = new ApplyCouponEvent(CustomerId, couponCode);
            await _mediatorHandler.PublishEvent(evnt);

            if (IsOperationValid())
            {
                return RedirectToAction("Index");
            }

            return View("Index", await _orderService.GetCustomerCart(CustomerId));
        }

        [Route("order-summary")]
        public async Task<IActionResult> OrderSummary()
        {
            return View(await _orderService.GetCustomerCart(CustomerId));
        }

        [HttpPost]
        [Route("create-order")]
        public async Task<IActionResult> CreateOrder(CartViewModel cartViewModel)
        {
            var cart = await _orderService.GetCustomerCart(CustomerId);

            var command = new StartOrderEvent(CustomerId, cart.OrderId, cart.TotalPrice, cartViewModel.Payment.CardName,
                cartViewModel.Payment.CardNumber, cartViewModel.Payment.CardExpirationDate, cartViewModel.Payment.CardVerificationCode);

            await _mediatorHandler.PublishEvent(command);

            if (IsOperationValid())
            {
                return RedirectToAction("Index", "Order");
            }

            return View("OrderSummary", await _orderService.GetCustomerCart(CustomerId));
        }
    }
}
