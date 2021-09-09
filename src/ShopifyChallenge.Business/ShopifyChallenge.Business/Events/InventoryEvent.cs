using System;
using ShopifyChallenge.Core.Communication.Messages.DomainEvents;

namespace ShopifyChallenge.Catalog.Application.Events
{
    public class InventoryEvent : DomainEvent
    {
        public int RemainingQuantity { get; private set; }

        public InventoryEvent(Guid aggregateId, int remainingQuantity) : base(aggregateId)
        {
            RemainingQuantity = remainingQuantity;
        }
    }
}
