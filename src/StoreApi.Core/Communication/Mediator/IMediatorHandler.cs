using StoreApi.Core.Communication.Messages.DomainEvents;
using StoreApi.Core.Communication.Messages.Notifications;
using System.Threading.Tasks;
using StoreApi.Core.Communication.Messages;

namespace StoreApi.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T events) where T : Event;
        Task<bool> SendCommand<T>(T command) where T : Command;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
