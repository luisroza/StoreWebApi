using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Sales.Application.Commands;
using ShopifyChallenge.Sales.Application.Services;
using ShopifyChallenge.Sales.Application.ViewModels;
using System;
using System.Threading.Tasks;
using ShopifyChallenge.WebAPI.ViewModels;

namespace ShopifyChallenge.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IMediatorHandler _mediatorHandler;

        public CartController(INotificationHandler<DomainNotification> notifications,
                              IProductService productAppService,
                              IMediatorHandler mediatorHandler,
                              IOrderService orderQueries,
                              IUser user)
                                : base(notifications, mediatorHandler, user)
        {
            _productService = productAppService;
            _orderService = orderQueries;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }

        [HttpPost]
        [Route("add-item")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            if (product.InventoryQuantity < quantity)
            {
                return CustomResponse();
            }

            var command = new AddOrderLineCommand(UserId, product.Id, product.Name, quantity, product.Price);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return BadRequest();
            }
            
            return CustomResponse();
        }

        [HttpPost]
        [Route("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            var command = new RemoveOrderLineCommand(UserId, id);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return BadRequest();
            }

            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }

        [HttpPost]
        [Route("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productService.GetById(id);
            if (product == null) return BadRequest();

            var command = new UpdateOrderLineCommand(UserId, id, quantity);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return BadRequest();
            }

            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }

        [HttpPost]
        [Route("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyCouponCommand(UserId, voucherCode);
            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return BadRequest();
            }

            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }

        [Route("order-summary")]
        public async Task<IActionResult> OrderSummary()
        {
            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }

        [HttpGet]
        [Route("start-order")]
        public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
        {
            var cart = await _orderService.GetCustomerCart(UserId);

            var command = new StartOrderCommand(cart.OrderId, UserId, cart.TotalPrice, cartViewModel.Payment.CardName,
                cartViewModel.Payment.CardNumber, cartViewModel.Payment.CardExpirationDate, cartViewModel.Payment.CardVerificationCode);

            await _mediatorHandler.SendCommand(command);

            if (IsValidOperation())
            {
                return CustomResponse();
            }

            return CustomResponse(await _orderService.GetCustomerCart(UserId));
        }
    }
}
