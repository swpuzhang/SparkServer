using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Domain.Events
{
    public class SangongEvent : Event
    {
        public SangongEvent()
        {
        }

        public SangongEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
