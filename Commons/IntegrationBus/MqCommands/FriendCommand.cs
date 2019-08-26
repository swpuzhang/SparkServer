using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.MqCommands
{
    public class GetFriendInfoMqCommand
    {
        public GetFriendInfoMqCommand(long id, long otherId)
        {
            Id = id;
            OtherId = otherId;
        }

        public long Id { get; set; }
        public long OtherId { get; set; }
    }
    public class GetFriendInfoMqResponse
    {
        public GetFriendInfoMqResponse(FriendTypes friendType)
        {
            FriendType = friendType;
        }

        public FriendTypes FriendType { get; set; }
    }
}
