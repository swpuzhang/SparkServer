using Account.Domain.Models;
using Commons.Domain.Commands;
using Commons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Commands
{
    public class GetGameInfoCommand : Command<HasBodyResponse<GameInfo>>
    {
        public long Id { get; private set; }
        public GetGameInfoCommand(long id)
        {
            Id = id;
        }
    }
}
