using Newtonsoft.Json;
using Sangong.GameMessage;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;


namespace Commons.Domain.Models
{
    public interface IGameAction
    {
        
        string ActionName { get; set; }
        DateTime ActionTime { get; set; }
    }
    public class GameStartAct : IGameAction
    {
        public class PlayerInfo
        {
            [JsonConstructor]
            public PlayerInfo(long id, long carry, int seatNum, List<PokerCard> handCards)
            {
                Id = id;
                Carry = carry;
                SeatNum = seatNum;
                HandCards = handCards;
            }

            public long Id { get; set; }

            public long Carry { get; set; }
            public int SeatNum { get; set; }
            public List<PokerCard> HandCards { get; set; }


        }

        [JsonConstructor]
        public GameStartAct(string roomId, long blind, List<PlayerInfo> players)
        {
            RoomId = roomId;
            Blind = blind;
            Players = players;
        }

        public string ActionName { get; set; } = "GameStar";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public string RoomId { get; set; }
        public long Blind { get; set; }
        public List<PlayerInfo> Players { get; set; }
    }

    public class GameActiveAct : IGameAction
    {
        [JsonConstructor]
        public GameActiveAct(long id, int activeSeatNum)
        {
            Id = id;
            ActiveSeatNum = activeSeatNum;
        }

        public string ActionName { get; set; } = "GameActive";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int ActiveSeatNum { get; set; }

    }

    public class DealThirdCardAct : IGameAction
    {
        [JsonConstructor]
        public DealThirdCardAct(List<PlayerInfo> players)
        {
            Players = players;
        }

        public class PlayerInfo
        {
            [JsonConstructor]
            public PlayerInfo(long id, int seatNum, List<PokerCard> handCards, int cardType, int point)
            {
                Id = id;
                SeatNum = seatNum;
                HandCards = handCards;
                CardType = cardType;
                Point = point;
            }

            public long Id { get; set; }
            public int SeatNum { get; set; }
            public List<PokerCard> HandCards { get; set; }
            public int CardType { get; set; }
            public int Point { get; set; }
        }
        public string ActionName { get; set; } = "DealThirdCard";
        public DateTime ActionTime { get; set; } = DateTime.Now;

        public List<PlayerInfo> Players { get; set; }

    }

    public class DropAct : IGameAction
    {
        [JsonConstructor]
        public DropAct(long id, int seatNum)
        {
            Id = id;
            SeatNum = seatNum;
        }

        public string ActionName { get; set; } = "Drop";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int SeatNum { get; set; }

    }

    public class PassAct : IGameAction
    {
        [JsonConstructor]
        public PassAct(long id, int seatNum)
        {
            Id = id;
            SeatNum = seatNum;
        }

        public string ActionName { get; set; } = " Pass";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int SeatNum { get; set; }
    }

    public class FollowAct : IGameAction
    {
        [JsonConstructor]
        public FollowAct(long id, int seatNum, long followCoins, long carry)
        {
            Id = id;
            SeatNum = seatNum;
            FollowCoins = followCoins;
            Carry = carry;
        }

        public string ActionName { get; set; } = "Follow";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int SeatNum { get; set; }
        public long FollowCoins { get; set; }
        public long Carry { get; set; }
    }

    public class AddAct : IGameAction
    {
        [JsonConstructor]
        public AddAct(long id, int seatNum, long addCoins, long carry)
        {
            Id = id;
            SeatNum = seatNum;
            AddCoins = addCoins;
            Carry = carry;
        }

        public string ActionName { get; set; } = "Add";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int SeatNum { get; set; }
        public long AddCoins { get; set; }
        public long Carry { get; set; }
    }

    public class StandupAct : IGameAction
    {
        [JsonConstructor]
        public StandupAct(long id, int seatNum, long addCoins, long carry)
        {
            Id = id;
            SeatNum = seatNum;
        }

        public string ActionName { get; set; } = "Standup";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public long Id { get; set; }
        public int SeatNum { get; set; }
    }

    public class GameOverAct : IGameAction
    {
        public GameOverAct(List<int> winnerSeats, List<WinnerCoinsPool> winnerPool, List<PlayerInfo> players)
        {
            WinnerSeats = winnerSeats;
            WinnerPool = winnerPool;
            Players = players;
        }

        public class PlayerInfo
        {
            public PlayerInfo(long id, int seatNum, int coinsIncrease, PlayerCards handCards)
            {
                Id = id;
                SeatNum = seatNum;
                CoinsIncrease = coinsIncrease;
                HandCards = handCards;
            }

            public long Id { get; set; }
            public int SeatNum { get; set; }

            public int CoinsIncrease { get; set; }

            public PlayerCards HandCards { get; set; }

        }
        public string ActionName { get; set; } = "GameOver";
        public DateTime ActionTime { get; set; } = DateTime.Now;
        public List<int> WinnerSeats { get; set; }
        public List<WinnerCoinsPool> WinnerPool { get; set; }
        public List<PlayerInfo> Players { get; set; }
    }

    public class SangongGameLog
    {
        public static string gameStart = "GameStart";
        public static string gameActive = "GameActive";
        public static string dealThirdCard = "DealThirdCard";
        public static string drop = "Drop";
        public static string pass = "Pass";
        public static string follow = "Follow";
        public static string add = "Add";
        public static string standup = "Standup";
        public static string gameOver = "GameOver";

        public string GameId { get; set; }
        public long Blind { get; set; }
        public string RoomId { get; set; }
        public RoomTypes RoomType { get; set; }
        public DateTime GameTime { get; set; }
        public List<IGameAction> GameActions { get; set; }
    }
}
