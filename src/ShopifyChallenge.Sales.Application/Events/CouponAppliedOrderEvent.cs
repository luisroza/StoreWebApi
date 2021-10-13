﻿using Store.Core.Communication.Messages;
using System;

namespace Store.Sales.Application.Events
{
    public class CouponAppliedOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid CouponId { get; private set; }

        public CouponAppliedOrderEvent(Guid customerId, Guid orderId, Guid couponId)
        {
            CouponId = couponId;
            CustomerId = customerId;
            OrderId = orderId;
        }
    }
}
