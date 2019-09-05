using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSample.Domain.Events
{
    public class GameSampleEvent : Event
    {
        public GameSampleEvent()
        {
        }

        public GameSampleEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
