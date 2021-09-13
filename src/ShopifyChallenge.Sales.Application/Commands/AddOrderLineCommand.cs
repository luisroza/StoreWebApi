using System;
using FluentValidation;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class AddOrderLineCommand : Command
    {
        public Guid CustomerId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }

        public AddOrderLineCommand(Guid customerId, Guid productId, string nome, int quantity, decimal unityPrice)
        {
            CustomerId = customerId;
            ProductId = productId;
            Name = nome;
            Quantity = quantity;
            UnitPrice = unityPrice;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderLineValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class AddOrderLineValidation : AbstractValidator<AddOrderLineCommand>
    {
        public AddOrderLineValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer ID");

            RuleFor(c => c.ProductId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid product ID");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("Product's name not given");

            RuleFor(c => c.Quantity)
                .GreaterThan(0)
                .WithMessage("Minimum quantity: 1");

            RuleFor(c => c.UnitPrice)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero");
        }
    }
}
