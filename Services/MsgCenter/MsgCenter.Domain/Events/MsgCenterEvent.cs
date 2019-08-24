using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgCenter.Domain.Events
{
    public class MsgCenterEvent : Event
    {
        public MsgCenterEvent()
        {
        }

        public MsgCenterEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
