﻿using Store.Core.Communication.Messages;
using System;

namespace Store.Sales.Application.Events
{
    public class DraftOrderStartedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public DraftOrderStartedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
