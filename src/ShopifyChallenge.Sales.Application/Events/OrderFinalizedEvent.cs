using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class OrderFinalizedEvent : Event
    {
        public Guid OrderId { get; private set; }

        public OrderFinalizedEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }

        public override bool IsValid()
        {
            ValidationResult = new OrderFinalizedValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class OrderFinalizedValidation : AbstractValidator<OrderFinalizedEvent>
    {
        public OrderFinalizedValidation()
        {
            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");
        }
    }
}
