using Commons.Domain.Events;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Commands
{
    public abstract class Command<TResponse> : IRequest<TResponse>
    {
        public DateTime TimeStamp { get; private set; }
       
        public string CommandType { get; protected set; }

        public Guid AggregateId { get; protected set; }

        //public ValidationResult ValidationResult { get; set; }

        public Command()
        {
            CommandType = GetType().Name;

            TimeStamp = DateTime.Now;

            AggregateId = Guid.NewGuid();
        }
        
        //public abstract bool IsValid();
    }
}
