using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Sangong.MqCommands;

namespace ServerLog.Domain.Models
{
                 
    public class GameLogInfo
    {

       

        public GameLogInfo()
        {

        }

        public GameLogInfo(string gameId, long blind, string roomId, 
            RoomTypes roomType, DateTime gameTime, List<IGameAction> gameActions)
        {
            GameId = gameId;
            Blind = blind;
            RoomId = roomId;
            RoomType = roomType;
            GameTime = gameTime;
            GameActions = gameActions;
        }

        public string GameId { get; set; }
        public long Blind { get; set; }
        public string RoomId { get; set; }
        public RoomTypes RoomType { get; set; }
        public DateTime GameTime { get; set; }
        public List<IGameAction> GameActions { get; set; }

    }

    public class MoneyLogInfo
    {

        public MoneyLogInfo(long id, long curCoins, long curDiamonds,
            long maxCoins, long maxDiamonds,
            long coinsChangeCount, long diamondsChangeCount)
        {
            Id = id;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxCoins = maxCoins;
            MaxDiamonds = maxDiamonds;
            CoinsChangeCount = coinsChangeCount;
            DiamondsChangeCount = diamondsChangeCount;
        }

        public long Id { get; set; }
        public long CurCoins { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxCoins { get; set; }
        public long MaxDiamonds { get; set; }
        public long CoinsChangeCount { get; set; }
        public long DiamondsChangeCount { get; set; }
        public AddReason reason { get; set; }

    }
}
