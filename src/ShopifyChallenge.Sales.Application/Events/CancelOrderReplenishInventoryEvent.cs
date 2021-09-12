using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class CancelOrderReplenishInventoryEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderReplenishInventoryEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderReplenishInventoryValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderReplenishInventoryValidation : AbstractValidator<CancelOrderReplenishInventoryEvent>
    {
        public CancelOrderReplenishInventoryValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");
        }
    }
}
