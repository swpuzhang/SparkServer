using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Reward.Domain.Models;

namespace Reward.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<RewardResponse>
    public class GetRegisterRewardCommand : Command<BodyResponse<RewardInfoVM>>
    {
        public long Id  { get; private set; }
        public GetRegisterRewardCommand(long id)
        {
            Id = id;
        }
    }

    public class QueryRegisterRewardCommand : Command<BodyResponse<RegisterRewardVM>>
    {
        public long Id { get; private set; }
        public QueryRegisterRewardCommand(long id)
        {
            Id = id;
        }
    }

    public class QueryLoginRewardCommand : Command<BodyResponse<LoginRewardVM>>
    {
        public long Id { get; private set; }
        public QueryLoginRewardCommand(long id)
        {
            Id = id;
        }
    }

    public class GetLoginRewardCommand : Command<BodyResponse<RewardInfoVM>>
    {
        public long Id { get; private set; }
        public GetLoginRewardCommand(long id)
        {
            Id = id;
        }
    }

    public class QueryBankruptcyCommand : Command<BodyResponse<BankruptcyInfoVM>>
    {
        public long Id { get; private set; }
        public QueryBankruptcyCommand(long id)
        {
            Id = id;
        }
    }

    public class GetBankruptcyRewardCommand : Command<BodyResponse<RewardInfoVM>>
    {
        public long Id { get; private set; }
        public GetBankruptcyRewardCommand(long id)
        {
            Id = id;
        }
    }
}
