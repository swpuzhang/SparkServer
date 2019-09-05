using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Dummy.Domain.Models;
using Dummy.MqCommands;

namespace Dummy.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<DummyMatchingResponseInfo>
    public class DummyPlaynowCommand : Command<BodyResponse<DummyMatchingResponseInfo>>
    {
        public long Id { get; private set; }
        public DummyPlaynowCommand(long id)
        {
            Id = id;
        }
    }

    public class BlindMatchingCommand : Command<BodyResponse<DummyMatchingResponseInfo>>
    {
        public long Id { get; private set; }
        public long Blind { get; private set; }
        public BlindMatchingCommand(long id, long blind)
        {
            Id = id;
            Blind = blind;
        }
    }
}
