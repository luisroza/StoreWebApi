using System;

namespace StoreApi.Core.Communication.Messages.IntegrationEvents
{
    public class OrderInventoryRejectedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public OrderInventoryRejectedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
