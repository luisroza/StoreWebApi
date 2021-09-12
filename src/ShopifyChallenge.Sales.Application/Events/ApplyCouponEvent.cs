using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class ApplyCouponEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public string CouponCode { get; private set; }

        public ApplyCouponEvent(Guid customerId, string couponCode)
        {
            CustomerId = customerId;
            CouponCode = couponCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new ApplyCouponValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class ApplyCouponValidation : AbstractValidator<ApplyCouponEvent>
    {
        public ApplyCouponValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.CouponCode)
                .NotEmpty()
                .WithMessage("Coupons' code cannot be empty");
        }
    }
}
