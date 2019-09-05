using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using GameSample.Domain.Models;
using GameSample.MqCommands;

namespace GameSample.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<GameSampleMatchingResponseInfo>
    public class GameSamplePlaynowCommand : Command<BodyResponse<GameSampleMatchingResponseInfo>>
    {
        public long Id { get; private set; }
        public GameSamplePlaynowCommand(long id)
        {
            Id = id;
        }
    }

    public class BlindMatchingCommand : Command<BodyResponse<GameSampleMatchingResponseInfo>>
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
