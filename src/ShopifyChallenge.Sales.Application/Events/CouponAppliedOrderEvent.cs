using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
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

        public override bool IsValid()
        {
            ValidationResult = new CouponAppliedOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CouponAppliedOrderValidation : AbstractValidator<CouponAppliedOrderEvent>
    {
        public CouponAppliedOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");

            RuleFor(c => c.CouponId)
                .NotEqual(Guid.Empty)
                .WithMessage("Coupon Id is invalid");
        }
    }
}
