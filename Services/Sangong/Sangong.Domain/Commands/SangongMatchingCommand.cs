using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Sangong.Domain.Models;
using Sangong.MqCommands;

namespace Sangong.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<SangongMatchingResponseInfo>
    public class SangongPlaynowCommand : Command<BodyResponse<SangongMatchingResponseInfo>>
    {
        public long Id { get; private set; }
        public SangongPlaynowCommand(long id)
        {
            Id = id;
        }
    }
     
}
