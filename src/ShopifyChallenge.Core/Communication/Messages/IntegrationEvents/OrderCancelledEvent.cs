using ShopifyChallenge.Core.DomainObjects.DTO;
using System;

namespace ShopifyChallenge.Core.Communication.Messages.IntegrationEvents
{
    public class OrderCancelledEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public OrderItemList OrderProducts { get; private set; }

        public OrderCancelledEvent(Guid orderId, Guid customerId, OrderItemList orderProducts)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            OrderProducts = orderProducts;
        }
    }
}
