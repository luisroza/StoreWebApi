using ShopifyChallenge.Core.Communication;
using System;
using FluentValidation;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class RemoveOrderLineEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }

        public RemoveOrderLineEvent(Guid customerId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            ProductId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
        }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class RemoveOrderLineValidation : AbstractValidator<RemoveOrderLineEvent>
    {
        public RemoveOrderLineValidation()
        {
            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");

            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customer Id is invalid");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Product Id is invalid");
        }
    }
}
