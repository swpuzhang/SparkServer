using Account.Domain.Models;
using Commons.Domain.Commands;
using Commons.Domain.Models;
using Commons.MqCommands;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Commands
{
    public class GetSelfAccountCommand : Command<BodyResponse<AccountDetail>>
    {
        public long Id { get; private set; }
        public GetSelfAccountCommand(long id)
        {
            Id = id;
        }
    }

    public class GetAccountBaseInfoCommand : Command<BodyResponse<AccountInfo>>
    {
        public long Id { get; private set; }
        public GetAccountBaseInfoCommand(long id)
        {
            Id = id;
        }
    }

    public class GetIdByPlatformCommand : Command<BodyResponse<GetIdByPlatformMqResponse>>
    {
        public string PlatformAccount { get; private set; }
        public int Type { get; private set; }
        public GetIdByPlatformCommand(string platformAccount, int type)
        {
            PlatformAccount = platformAccount;
            Type = type;
        }
    }
}
