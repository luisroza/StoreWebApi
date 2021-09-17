using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Sales.Application.Services;
using ShopifyChallenge.WebAPI.ViewModels;
using System.Threading.Tasks;
using ShopifyChallenge.Sales.Application.Commands;

namespace ShopifyChallenge.WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;
        private readonly IMediatorHandler _mediatorHandler;

        public OrderController(INotificationHandler<DomainNotification> notification,
                                IOrderService orderService,
                                IMediatorHandler mediatorHandler,
                                IUser user) : base(notification, mediatorHandler, user)
        {
            _orderService = orderService;
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return CustomResponse(await _orderService.GetCustomerOrders(UserId));
        }

        [HttpPut("{id:guid}")]
        [Route("finalize-order")]
        public async Task<IActionResult> SetOrderFinished(Guid id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return BadRequest();

            var command = new FinalizeOrderCommand(id, UserId);
            await _mediatorHandler.SendCommand(command);

            return IsValidOperation() ? BadRequest() : CustomResponse(order);
        }

        [HttpPut("{id:guid}")]
        [Route("cancel-order")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return BadRequest();

            var command = new CancelOrderCommand(id, UserId);
            await _mediatorHandler.SendCommand(command);

            return IsValidOperation() ? BadRequest() : CustomResponse(order);
        }

        [HttpPut("{id:guid}")]
        [Route("make-draft-order")]
        public async Task<IActionResult> MakeDraftOrder(Guid id)
        {
            var order = await _orderService.GetById(id);
            if (order == null) return BadRequest();

            var command = new CancelOrderReplenishInventoryCommand(id, UserId);
            await _mediatorHandler.SendCommand(command);

            return IsValidOperation() ? BadRequest() : CustomResponse(order);
        }
    }
}
