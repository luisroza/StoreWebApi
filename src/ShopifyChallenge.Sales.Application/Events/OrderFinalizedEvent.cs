using ShopifyChallenge.Core.Communication.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class OrderFinalizedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderFinalizedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }
    }
}
