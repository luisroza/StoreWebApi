using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class CancelOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderEvent(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderValidation : AbstractValidator<CancelOrderEvent>
    {
        public CancelOrderValidation()
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
