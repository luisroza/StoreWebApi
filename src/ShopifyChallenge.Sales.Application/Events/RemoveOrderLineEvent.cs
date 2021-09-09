﻿using ShopifyChallenge.Core.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class RemoveOrderLineEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }

        public RemoveOrderLineEvent(Guid customerId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            ProductId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}