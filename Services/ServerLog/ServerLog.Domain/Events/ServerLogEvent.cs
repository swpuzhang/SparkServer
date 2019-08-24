using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServerLog.Domain.Events
{
    public class ServerLogEvent : Event
    {
        public ServerLogEvent()
        {
        }

        public ServerLogEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
