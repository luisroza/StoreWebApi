using ShopifyChallenge.Core.Communication.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class UpdateOrderLineEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateOrderLineEvent(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
