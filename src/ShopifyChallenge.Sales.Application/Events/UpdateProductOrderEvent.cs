using FluentValidation;
using ShopifyChallenge.Core.Communication;
using System;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class UpdateProductOrderEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public UpdateProductOrderEvent(Guid customerId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            ProductId = productId;
            CustomerId = customerId;
            OrderId = orderId;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProductOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateProductOrderValidation : AbstractValidator<UpdateProductOrderEvent>
    {
        public UpdateProductOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Products' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum quantity for an item is 1");
        }
    }
}
