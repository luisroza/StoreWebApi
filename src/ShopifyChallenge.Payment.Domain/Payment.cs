using ShopifyChallenge.Core.DomainObjects;
using System;

namespace ShopifyChallenge.Payment.Domain
{
    public class Payment : Entity, IAggregateRoot
    {
        public Guid OrderId { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardVerificationCode { get; set; }

        //EF Relation
        public Transaction Transaction { get; set; }
    }
}
