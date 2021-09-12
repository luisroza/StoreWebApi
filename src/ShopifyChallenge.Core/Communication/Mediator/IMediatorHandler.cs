﻿using ShopifyChallenge.Core.Communication.Messages.DomainEvents;
using ShopifyChallenge.Core.Communication.Messages.Notifications;
using System.Threading.Tasks;

namespace ShopifyChallenge.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishEvent<T>(T events) where T : Event;
        Task PublishNotification<T>(T notification) where T : DomainNotification;
        Task PublishDomainEvent<T>(T domainEvent) where T : DomainEvent;
    }
}
