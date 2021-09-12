using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using ShopifyChallenge.Sales.Application.Services;
using System.Threading.Tasks;

namespace ShopifyChallenge.WebApp.Controllers
{
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService,
                                INotificationHandler<DomainNotification> notifications,
                                IMediatorHandler mediatorHandler) : base(notifications, mediatorHandler)
        {
            _orderService = orderService;
        }

        [Route("my-orders")]
        public async Task<IActionResult> Index()
        {
            return View(await _orderService.GetCustomerOrders(CustomerId));
        }
    }
}
