using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.IntegrationBus.MqEvents
{
    public class MqEvent
    {
        public string EventType { get; set; }

        public Guid AggregateId { get; set; }

        public DateTime Timestamp { get; set; }
        protected MqEvent()
        {
            EventType = GetType().Name;
            Timestamp = DateTime.Now;
        }
    }
}
