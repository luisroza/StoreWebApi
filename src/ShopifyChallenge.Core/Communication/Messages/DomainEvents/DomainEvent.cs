﻿using System;
using MediatR;

namespace Store.Core.Communication.Messages.DomainEvents
{
    public abstract class DomainEvent : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }
    }
}
