using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class UpdateOrderLineEvent : Event
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateOrderLineEvent(Guid customerId, Guid productId, int quantity)
        {
            CustomerId = customerId;
            ProductId = productId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateOrderLineValidation : AbstractValidator<UpdateOrderLineEvent>
    {
        public UpdateOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Products' Id is invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum quantity for an item is 1");
        }
    }
}
