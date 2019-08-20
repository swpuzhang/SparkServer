using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Reward.Domain.Models;
using Sangong.MqCommands;
using MediatR;

namespace Reward.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<RewardResponse>
    public class GameActivityCommand : Command<List<OneGameActivityInfoVM>>
    {
        public long Id  { get; private set; }
        public ActivityTypes Type { get; private set; }
        public GameActivityCommand(long id, ActivityTypes type)
        {
            Id = id;
            Type = type;
        }
    }

    public class GetGameActRewardCommand : Command<BodyResponse<RewardInfoVM>>
    {
        public GetGameActRewardCommand(long id, string actId, string subId)
        {
            Id = id;
            ActId = actId;
            SubId = subId;
        }

        public long Id { get; private set; }
        public string ActId { get; private set; }
        public string SubId { get; private set; }
    }

  
    public class AddActFromGamelogCommand : Command<Unit>
    {
        public Dictionary<long, long> _allPlayers { get; private set; }
        public RoomTypes RoomType { get; private set; }
        public AddActFromGamelogCommand(Dictionary<long, long> allPlayers, RoomTypes roomType)
        {
            _allPlayers = allPlayers;
            RoomType = roomType;
        }
    }
}
