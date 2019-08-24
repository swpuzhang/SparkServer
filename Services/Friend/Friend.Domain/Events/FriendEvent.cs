using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friend.Domain.Events
{
    public class FriendEvent : Event
    {
        public FriendEvent()
        {
        }

        public FriendEvent(Guid gid)
        {
            AggregateId = gid;
        }

       
    }
}
