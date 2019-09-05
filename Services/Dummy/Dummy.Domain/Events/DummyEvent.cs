using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dummy.Domain.Events
{
    public class DummyEvent : Event
    {
        public DummyEvent()
        {
        }

        public DummyEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
