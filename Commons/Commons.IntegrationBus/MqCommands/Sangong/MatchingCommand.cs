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

    public class JoinGameRoomMqCommand
    {
        public JoinGameRoomMqCommand(long id, int roomId, string gameKey)
        {
            Id = id;
            RoomId = roomId;
            GameKey = gameKey;
        }

        public long Id { get; private set; }
        public int RoomId { get; private set; }

        public string GameKey { get; private set; }
    }

    public class CreateRoomMqCommand
    {
        public CreateRoomMqCommand()
        {
        }

        public CreateRoomMqCommand(int roomId, string gameKey, long blind, long minCoins, long maxCoins, int tipsPersent)
        {
            RoomId = roomId;
            GameKey = gameKey;
            Blind = blind;
            MinCoins = minCoins;
            MaxCoins = maxCoins;
            TipsPersent = tipsPersent;
        }

        public int RoomId { get; private set; }
        public string GameKey { get; private set; }
        public long Blind { get; private set; }
        public long MinCoins { get; private set; }
        public long MaxCoins { get; private set; }
        public int TipsPersent { get; private set; }
    }
}
