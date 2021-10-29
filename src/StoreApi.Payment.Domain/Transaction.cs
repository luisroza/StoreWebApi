using StoreApi.Core.DomainObjects;
using System;

namespace StoreApi.Payment.Domain
{
    public class Transaction : Entity
    {
        public Guid OrderId { get; set; }
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public TransactionStatus TransactionStatus { get; set; }

        //EF Relation
        public Payment Payment { get; set; }
    }
}
