using MediatR;
using ShopifyChallenge.Core.Communication.Mediator;
using ShopifyChallenge.Core.Communication.Messages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderStartedEvent>,
        INotificationHandler<AddOrderLineEvent>,
        INotificationHandler<UpdateOrderEvent>,
        INotificationHandler<OrderInventoryRejectedEvent>,
        INotificationHandler<CheckOutEvent>,
        INotificationHandler<PaymentRejectedEvent>
    {
        private readonly IMediatorHandler _mediatorHandler;

        public OrderEventHandler(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        public Task Handle(DraftOrderStartedEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(AddOrderLineEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public Task Handle(UpdateOrderEvent message, CancellationToken cancellationToken)
        {
            //TODO: When needed use this to throw some event
            return Task.CompletedTask;
        }

        public async Task Handle(OrderInventoryRejectedEvent message, CancellationToken cancellationToken)
        {
            //cancel order process - show error to customer
            await _mediatorHandler.PublishEvent(new CancelOrderEvent(message.OrderId, message.CustomerId));
        }

        public async Task Handle(CheckOutEvent message, CancellationToken cancellationToken)
        {
            //Order completed
            await _mediatorHandler.PublishEvent(new FinalizeOrderEvent(message.CustomerId, message.CustomerId));
        }

        public async Task Handle(PaymentRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.PublishEvent(new CancelOrderReplenishInventoryEvent(message.OrderId, message.CustomerId));
        }
    }
}
