using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Domain.Events
{
    public class SampleEvent : Event
    {
        public SampleEvent()
        {
        }

        public SampleEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
