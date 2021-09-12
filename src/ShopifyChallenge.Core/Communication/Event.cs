using System;
using FluentValidation.Results;
using MediatR;
using ShopifyChallenge.Core.Communication.Messages;

namespace ShopifyChallenge.Core.Communication
{
    public abstract class Event : Message, INotification
    {
        public DateTime Timestamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Event()
        {
            Timestamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
