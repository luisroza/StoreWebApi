﻿using Store.Payment.Domain;

namespace Store.Payment.Application.AntiCorruption
{
    public class CreditCardPaymentFacade : ICreditCardPaymentFacade
    {
        private readonly IPayPalGateway _payPalGateway;
        private readonly IConfigurationManager _configurationManager;

        public CreditCardPaymentFacade(IPayPalGateway payPalGateway, IConfigurationManager configurationManager)
        {
            _payPalGateway = payPalGateway;
            _configurationManager = configurationManager;
        }
        public Transaction CheckOut(Order order, Domain.Payment payment)
        {
            var apiKey = _configurationManager.GetValue("apiKey");
            var encryptionKey = _configurationManager.GetValue("encryptionKey");

            var serviceKey = _payPalGateway.GetPayPalServiceKey(apiKey, encryptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);

            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Amount);

            //TODO: Payment gateway should return a transaction object
            var transaction = new Transaction
            {
                OrderId = order.Id,
                Amount = order.Amount,
                PaymentId = payment.OrderId
            };

            if (paymentResult)
            {
                transaction.TransactionStatus = TransactionStatus.Paid;
                return transaction;
            }

            transaction.TransactionStatus = TransactionStatus.Rejected;
            return transaction;
        }
    }
}
