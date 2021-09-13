using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Sales.Application.Services;
using System.Threading.Tasks;
using ShopifyChallenge.WebAPI.ViewModels;

namespace ShopifyChallenge.WebAPI.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(INotificationHandler<DomainNotification> notification,
            IOrderService orderService,
            IMediatorHandler mediatorHandler,
            IUser user) : base(notification, mediatorHandler, user)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            return CustomResponse(await _orderService.GetCustomerOrders(UserId));
        }
    }
}
