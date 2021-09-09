using ShopifyChallenge.Core.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class FinalizeOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public FinalizeOrderEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
