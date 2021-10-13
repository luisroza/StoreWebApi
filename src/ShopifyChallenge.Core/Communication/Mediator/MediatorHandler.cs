using MediatR;
using Store.Core.Communication.Messages;
using Store.Core.Communication.Messages.DomainEvents;
using Store.Core.Communication.Messages.Notifications;
using System.Threading.Tasks;

namespace Store.Core.Communication.Mediator
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

        public async Task<bool> SendCommand<T>(T command) where T : Command
        {
            //Send = request
            return await _mediator.Send(command);
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
