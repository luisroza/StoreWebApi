using System;
using FluentValidation;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class CancelOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderCommand(Guid orderId, Guid customerId)
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

    public class CancelOrderValidation : AbstractValidator<CancelOrderCommand>
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
