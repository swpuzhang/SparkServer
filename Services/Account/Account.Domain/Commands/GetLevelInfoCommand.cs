using Account.Domain.Models;
using Commons.Domain.Commands;
using Commons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Commands
{
    public class GetLevelInfoCommand : Command<HasBodyResponse<LevelInfo>>
    {
        public long Id { get; private set; }
        public GetLevelInfoCommand(long id)
        {
            Id = id;
        }
    }
}
