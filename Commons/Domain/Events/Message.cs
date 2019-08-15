using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Events
{
    public abstract class Message<TResponse> : IRequest<TResponse>
    {
        public string MessageType { get; protected set; }
        public Guid AggregateId { get; protected set; }

        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
