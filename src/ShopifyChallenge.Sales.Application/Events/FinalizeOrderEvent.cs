using FluentValidation;
using ShopifyChallenge.Core.Communication.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class FinalizeOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public FinalizeOrderEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new FinalizeOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class FinalizeOrderValidation : AbstractValidator<FinalizeOrderEvent>
    {
        public FinalizeOrderValidation()
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
