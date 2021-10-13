using System;
using FluentValidation.Results;
using MediatR;

namespace Store.Core.Communication.Messages
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
