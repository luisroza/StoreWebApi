using MediatR;
using Store.Core.Communication.Mediator;
using Store.Core.Communication.Messages.IntegrationEvents;
using Store.Sales.Application.Commands;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Sales.Application.Events
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
            await _mediatorHandler.SendCommand(new CancelOrderCommand(message.OrderId, message.CustomerId));
        }

        public async Task Handle(CheckOutEvent message, CancellationToken cancellationToken)
        {
            //Order completed
            await _mediatorHandler.SendCommand(new FinalizeOrderCommand(message.CustomerId, message.CustomerId));
        }

        public async Task Handle(PaymentRejectedEvent message, CancellationToken cancellationToken)
        {
            await _mediatorHandler.SendCommand(new CancelOrderReplenishInventoryCommand(message.OrderId, message.CustomerId));
        }
    }
}
