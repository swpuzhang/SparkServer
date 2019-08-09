﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.MqCommands
{
    public class RoomIdMapConfigMqCommand
    {
        public RoomIdMapConfigMqCommand(List<KeyValuePair<string, string>> config)
        {
            Config = config;
        }

        public List<KeyValuePair<string, string>> Config { get; private set; }
    }


    public class CreateRoomMqCommand
    {
        public CreateRoomMqCommand()
        {
        }

        public CreateRoomMqCommand(string roomId, string gameKey,
            long blind, long minCoins, long maxCoins,
            int tipsPersent, int seatCount, long minCarry, long maxCarry)
        {
            RoomId = roomId;
            GameKey = gameKey;
            Blind = blind;
            MinCoins = minCoins;
            MaxCoins = maxCoins;
            TipsPersent = tipsPersent;
            SeatCount = seatCount;
            MinCarry = minCarry;
            MaxCarry = maxCarry;
        }

        public string RoomId { get; private set; }
        public string GameKey { get; private set; }
        public long Blind { get; private set; }
        public long MinCoins { get; private set; }
        public long MaxCoins { get; private set; }
        public int TipsPersent { get; private set; }
        public int SeatCount { get; private set; }

        public long MinCarry { get; private set; }

        public long MaxCarry { get; private set; }

    }

    public class JoinGameRoomMqCommand
    {
        public JoinGameRoomMqCommand(long id, string roomId, string gameKey)
        {
            Id = id;
            RoomId = roomId;
            GameKey = gameKey;
        }

        public long Id { get; private set; }
        public string RoomId { get; private set; }

        public string GameKey { get; private set; }
    }

    public class JoinGameRoomMqResponse
    {
        public JoinGameRoomMqResponse(long id, string roomId, string gameKey, int userCount, long blind)
        {
            Id = id;
            RoomId = roomId;
            GameKey = gameKey;
            UserCount = userCount;
            Blind = blind;
        }

        public long Id { get; private set; }
        public string RoomId { get; private set; }

        public string GameKey { get; private set; }

        public int UserCount { get; private set; }

        public long Blind { get; private set; }
    }

    public class SyncRoomInfo
    {
        public SyncRoomInfo(string roomId, int userCount, long blind)
        {
            RoomId = roomId;
            UserCount = userCount;
            Blind = blind;
        }

        public string RoomId { get; private set; }

        public int UserCount { get; private set; }

        public long Blind { get; private set; }

    }

    

    public class SyncGameRoomMqCommand
    {

        public SyncGameRoomMqCommand(string gameKey, string matchingGroup, List<SyncRoomInfo> syncInfo)
        {

            GameKey = gameKey;
            SyncInfo = syncInfo;
            MatchingGroup = matchingGroup;
        }
        public string GameKey { get; private set; }
        public string MatchingGroup { get; private set; }
        
        public List<SyncRoomInfo> SyncInfo { get; private set; }
        
    }

    public class UserApplySitMqCommand
    {
        public UserApplySitMqCommand(long id, string roomId, string gameKey, long blind)
        {
            Id = id;
            RoomId = roomId;
            GameKey = gameKey;
            Blind = blind;
        }

        public long Id { get; private set; }
        public string RoomId { get; private set; }

        public string GameKey { get; private set; }

        public long Blind { get; private set; }
    }

    
}
