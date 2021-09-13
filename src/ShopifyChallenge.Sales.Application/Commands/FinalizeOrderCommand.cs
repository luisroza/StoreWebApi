using ShopifyChallenge.Core.Communication.Messages;
using System;

namespace ShopifyChallenge.Sales.Application.Commands
{
    public class FinalizeOrderCommand : Command
    {
        public Guid OrderId { get; private set; }
        public Guid CustomerId { get; private set; }

        public FinalizeOrderCommand(Guid orderId, Guid customerId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            CustomerId = customerId;
        }
    }
}
