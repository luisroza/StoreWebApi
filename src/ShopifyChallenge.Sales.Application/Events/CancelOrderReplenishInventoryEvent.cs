using ShopifyChallenge.Core.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class CancelOrderReplenishInventoryEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderReplenishInventoryEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
