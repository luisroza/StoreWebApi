using ShopifyChallenge.Core.Communication.Messages;
using System;
using FluentValidation;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class FinalizeOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public FinalizeOrderCommand(Guid orderId, Guid customerId)
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

    public class FinalizeOrderValidation : AbstractValidator<FinalizeOrderCommand>
    {
        public FinalizeOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Orders' Id is invalid");
        }
    }
}
