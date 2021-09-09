using ShopifyChallenge.Core.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class CouponAppliedOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid VoucherId { get; private set; }

        public CouponAppliedOrderEvent(Guid customerId, Guid orderId, Guid voucherId)
        {
            VoucherId = voucherId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
