﻿using FluentValidation.Results;
using MediatR;
using System;

namespace StoreApi.Core.Communication.Messages
{
    public abstract class Command : Message, IRequest<bool>
    {
        public DateTime TimeStamp { get; private set; }
        public ValidationResult ValidationResult { get; set; }

        protected Command()
        {
            TimeStamp = DateTime.Now;
        }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
