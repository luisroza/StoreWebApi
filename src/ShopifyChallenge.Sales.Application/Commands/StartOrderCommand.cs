using System;
using FluentValidation;
using Store.Core.Communication.Messages;

namespace Store.Sales.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpirationDate { get; private set; }
        public string CardVerificationCode { get; private set; }

        public StartOrderCommand(Guid orderId, Guid customerId, decimal total, string cardName, string cardNumber,
            string cardExpirationDate, string cardVerificationCode)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpirationDate = cardExpirationDate;
            CardVerificationCode = cardVerificationCode;
        }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(c => c.CustomerId)
                .NotEqual(Guid.Empty)
                .WithMessage("Customers' Id is invalid");

            RuleFor(c => c.OrderId)
                .NotEqual(Guid.Empty)
                .WithMessage("Orders' Id is invalid");

            RuleFor(c => c.CardName)
                .NotEmpty()
                .WithMessage("The on the card was not informed");

            RuleFor(c => c.CardNumber)
                .CreditCard()
                .WithMessage("Card number is invalid");

            RuleFor(c => c.CardExpirationDate)
                .NotEmpty()
                .WithMessage("Expiration date was not informed");

            RuleFor(c => c.CardVerificationCode)
                .Length(3, 4)
                .WithMessage("Card verification code was not informed");
        }
    }
}
