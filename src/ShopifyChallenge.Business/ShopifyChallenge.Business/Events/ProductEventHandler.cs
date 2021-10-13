using MediatR;
using Store.Catalog.Application.Services;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Catalog.Application.Events
{
    public class ProductEventHandler : INotificationHandler<OrderStartedEvent>,
        INotificationHandler<OrderCancelledEvent>
    {
        private readonly IInventoryService _inventoryService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IInventoryService inventoryService, IMediatorHandler mediatorHandler)
        {
            _inventoryService = inventoryService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(OrderStartedEvent message, CancellationToken cancellationToken)
        {
            var result = await _inventoryService.DecreaseInventoryProductList(message.ItemList);

            if (result)
            {
                await _mediatorHandler.PublishEvent(new OrderInventoryConfirmedEvent(message.OrderId, message.CustomerId, message.ItemList,
                    message.Total, message.CardName, message.CardNumber, message.CardExpirationDate, message.CardVerificationCode)); ;
            }
            else
            {
                await _mediatorHandler.PublishEvent(new OrderInventoryRejectedEvent(message.OrderId, message.CustomerId));
            }
        }

        public async Task Handle(OrderCancelledEvent message, CancellationToken cancellationToken)
        {
            await _inventoryService.AddInventoryOrderProductsList(message.OrderProducts);
        }
    }
}
