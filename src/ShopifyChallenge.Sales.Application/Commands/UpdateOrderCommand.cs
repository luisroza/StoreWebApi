using System;
using FluentValidation;
using Store.Core.Communication.Messages;

namespace Store.Sales.Application.Commands
{
    public class UpdateOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal TotalPrice { get; private set; }

        public UpdateOrderCommand(Guid customerId, Guid orderId, decimal totalPrice)
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

    public class UpdateOrderValidation : AbstractValidator<UpdateOrderCommand>
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
