using System.Threading.Tasks;
using MediatR;
using ShopifyChallenge.Core.Communication.Messages.DomainEvents;
using ShopifyChallenge.Core.Communication.Messages.Notifications;

namespace ShopifyChallenge.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;

        public MediatorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEvent<T>(T events) where T : Event
        {
            await _mediator.Publish(events);
        }

        public async Task PublishNotification<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task PublishDomainEvent<T>(T notification) where T : DomainEvent
        {
            await _mediator.Publish(notification);
        }
    }
}
