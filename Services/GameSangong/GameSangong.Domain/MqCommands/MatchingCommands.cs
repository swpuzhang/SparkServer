using System;
using System.Collections.Generic;
using System.Text;

namespace GameSangong.Domain.MqCommands
{
    public class RoomIdMapConfigCommand
    {
        public RoomIdMapConfigCommand(List<KeyValuePair<int, string>> config)
        {
            Config = config;
        }

        public List<KeyValuePair<int, string>> Config { get; private set; }
    }
}
