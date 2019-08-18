using Commons.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reward.Domain.Events
{
    public class RewardEvent : Event
    {
        public RewardEvent()
        {
        }

        public RewardEvent(Guid gid)
        {
            AggregateId = gid;
        }
    }
    public class InvitedFriendEvent : Event
    {
        public long Id { get; private set; }
        public string PlatformAccount { get; private set; }
        public int Type { get; private set; }
        public InvitedFriendEvent(long id, string platformAccount, int type)
        {
            Id = id;
            PlatformAccount = platformAccount;
            Type = type;
        }


    }

    public class InvitedFriendRegisterdEvent : Event
    {
        public string PlatformAccount { get; private set; }
        public int Type { get; private set; }
        public InvitedFriendRegisterdEvent(string platformAccount, int type)
        {
           
            PlatformAccount = platformAccount;
            Type = type;
        }
    }
}
