using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Events
{
    public class Event : INotification
    {
        public string EventType { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime Timestamp { get; set; }
        protected Event()
        {
            EventType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
