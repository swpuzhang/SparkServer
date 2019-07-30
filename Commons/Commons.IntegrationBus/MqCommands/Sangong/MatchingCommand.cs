using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.MqCommands
{
    public class RoomIdMapConfigMqCommand
    {
        public RoomIdMapConfigMqCommand(List<KeyValuePair<int, string>> config)
        {
            Config = config;
        }

        public List<KeyValuePair<int, string>> Config { get; private set; }
    }
}
