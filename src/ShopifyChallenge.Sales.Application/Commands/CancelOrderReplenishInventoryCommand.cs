﻿using System;
using FluentValidation;
using Store.Core.Communication.Messages;

namespace Store.Sales.Application.Commands
{
    public class CancelOrderReplenishInventoryCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public CancelOrderReplenishInventoryCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }

        public override bool IsValid()
        {
            ValidationResult = new CancelOrderReplenishInventoryValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class CancelOrderReplenishInventoryValidation : AbstractValidator<CancelOrderReplenishInventoryCommand>
    {
        public CancelOrderReplenishInventoryValidation()
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
