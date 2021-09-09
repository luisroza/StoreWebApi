using ShopifyChallenge.Core.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class CancelOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
