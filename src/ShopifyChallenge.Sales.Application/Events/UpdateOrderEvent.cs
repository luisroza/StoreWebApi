using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class UpdateOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }

        public UpdateOrderEvent(Guid customerId, Guid orderId, decimal totalPrice)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            TotalPrice = totalPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderValidation : AbstractValidator<UpdateOrderEvent>
    {
        public UpdateOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Orders' Id is invalid");

            RuleFor(c => c.TotalPrice)
                .GreaterThan(0)
                .WithMessage("Total order must be greater than zero");
        }
    }
}
