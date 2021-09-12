using FluentValidation;
using System;

namespace ShopifyChallenge.Core.Communication.Messages.IntegrationEvents
{
    public class CheckOutEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Amount { get; private set; }

        public CheckOutEvent(Guid orderId, Guid customerId, Guid paymentId, Guid transactionId, decimal amount)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Amount = amount;
        }
    }
}
