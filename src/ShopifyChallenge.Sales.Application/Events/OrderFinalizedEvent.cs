using Store.Core.Communication.Messages;
using System;

namespace Store.Sales.Application.Events
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
