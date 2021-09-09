using MediatR;
using System;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Core.Messages
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }
    }
}
