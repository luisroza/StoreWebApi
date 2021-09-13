using System;
using FluentValidation;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class ApplyCouponCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public string CouponCode { get; private set; }

        public ApplyCouponCommand(Guid customerId, string couponCode)
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

    public class ApplyCouponValidation : AbstractValidator<ApplyCouponCommand>
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
