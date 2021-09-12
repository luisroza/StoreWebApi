using ShopifyChallenge.Core.Communication;
using System;
using FluentValidation;

namespace ShopifyChallenge.Sales.Application.Events
{
    public class AddOrderLineEvent : Event
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public AddOrderLineEvent(Guid customerId, Guid orderId, Guid productId, string productName, decimal unitPrice, int quantity)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderLineValidation : AbstractValidator<AddOrderLineEvent>
    {
        public AddOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Order Id is invalid");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Unit price must be greater then 0");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Quantity must be greater than zero");
        }
    }
}
