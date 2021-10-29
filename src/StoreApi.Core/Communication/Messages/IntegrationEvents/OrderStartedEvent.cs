using StoreApi.Core.DomainObjects.DTO;
using System;

namespace StoreApi.Core.Communication.Messages.IntegrationEvents
{
    public class OrderStartedEvent : IntegrationEvent
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }
        public decimal Total { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpirationDate { get; private set; }
        public string CardVerificationCode { get; private set; }
        public OrderItemList ItemList { get; private set; }

        public OrderStartedEvent(Guid customerId, Guid orderId, OrderItemList itemList, decimal total, string cardName, string cardNumber, string cardExpirationDate, string cardVerificationCode)
        {
            AggregateId = orderId;
            CustomerId = customerId;
            OrderId = orderId;
            ItemList = itemList;
            Total = total;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpirationDate = cardExpirationDate;
            CardVerificationCode = cardVerificationCode;
        }
    }
}
