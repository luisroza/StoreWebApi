using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class DraftOrderStartedEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public DraftOrderStartedEvent(Guid customerId, Guid orderId)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }

        public override bool IsValid()
        {
            ValidationResult = new DraftOrderStartedValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class DraftOrderStartedValidation : AbstractValidator<DraftOrderStartedEvent>
    {
        public DraftOrderStartedValidation()
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
