using MediatR;
using ShopifyChallenge.Catalog.Application.Services;
using ShopifyChallenge.Catalog.Domain;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;
using StartOrderEvent = ShopifyChallenge.Sales.Application.Events.StartOrderEvent;

namespace ShopifyChallenge.Catalog.Application.Events
{
    public class ProductEventHandler : INotificationHandler<InventoryEvent>,
        INotificationHandler<StartOrderEvent>,
        INotificationHandler<OrderCancelledEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IInventoryService _inventoryService;
        private readonly IMediatorHandler _mediatorHandler;

        public ProductEventHandler(IProductRepository productRepository, IInventoryService inventoryService, IMediatorHandler mediatorHandler)
        {
            _productRepository = productRepository;
            _inventoryService = inventoryService;
            _mediatorHandler = mediatorHandler;
        }

        public async Task Handle(InventoryEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetById(message.AggregateId);

            // TODO: Send an e-mail to the team warning them to buy more products
        }

        public async Task Handle(StartOrderEvent message, CancellationToken cancellationToken)
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
            await _inventoryService.ReplenishInventoryOrderProductsList(message.OrderProducts);
        }
    }
}
