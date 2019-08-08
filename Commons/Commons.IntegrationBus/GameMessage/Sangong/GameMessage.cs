using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.GameMessage
{

    class GameMessage
    {
    }

    /// <summary>
    /// 玩家坐下广播事件
    /// </summary>
    public class PlayerSeatedEvent
    {
        public PlayerSeatedEvent()
        {
        }

        [JsonConstructor]
        public PlayerSeatedEvent(long userId, string userName, long curCoins, long curDiamonds, int seatNum, long carry)
        {
            UserId = userId;
            UserName = userName;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            SeatNum = seatNum;
            Carry = carry;
        }

        public long UserId { get; private set; }
        public string UserName { get; private set; }

        public long CurCoins { get; private set; }

        public long CurDiamonds { get; private set; }

        public int SeatNum { get; private set; }

        /// <summary>
        /// 牌桌买入
        /// </summary>
        public long Carry { get; private set; }
    }

    public class PokerCard : IComparable<PokerCard>
    {

        [JsonConstructor]
        public PokerCard(int point, int color)
        {
            Point = point;
            Color = color;
        }

        public PokerCard()
        {
        }

        public void SetCard(int point, int color)
        {
            Point = point;
            Color = color;
        }

        public int CompareTo(PokerCard other)
        {
            int ret = Point.CompareTo(other.Point);
            if (ret == 0)
            {
                return Color.CompareTo(other.Color);
            }
            return ret;
        }

        public int CompareFlush(PokerCard other)
        {
            int ret = Color.CompareTo(other.Color);
            if (ret == 0)
            {
                return Point.CompareTo(other.Point);
            }
            return ret;
        }

        public int Point { get; private set; }
        public int Color { get; private set; }
    }


    /// <summary>
    /// 广播发牌事件
    /// </summary>
    public class DealCardsEvent
    {
        public DealCardsEvent()
        {
        }

        [JsonConstructor]
        public DealCardsEvent(long dealerSeatNum, List<int> dealCardNum, int cardNum, 
            List<PokerCard> cards, long blind, List<long> carrys)
        {
            DealerSeatNum = dealerSeatNum;
            DealCardNum = dealCardNum;
            CardNum = cardNum;
            Cards = cards;
            Blind = blind;
            this.carrys = carrys;
        }

        /// <summary>
        /// 庄家的座位号
        /// </summary>
        public long DealerSeatNum { get; private set; }

        /// <summary>
        /// 发牌顺序，从庄家开始的座位号
        /// </summary>
        public List<int> DealCardNum { get; private set; }

        /// <summary>
        /// 手牌数量
        /// </summary>
        public int CardNum { get; private set; }

        /// <summary>
        /// 自己的手牌
        /// </summary>
        public List<PokerCard> Cards { get; private set; }

        /// <summary>
        /// 下底注
        /// </summary>
        public long Blind { get; private set; }

        /// <summary>
        /// 按发牌顺序,各个玩家的当前携带
        /// </summary>
        public List<long> carrys { get; private set; }
    }

    [Flags]
    public enum ActiveOperation
    {
        None = 0,
        Follow = 1,
        Drop = 2,
        Add = 3,
    }

    public class ActiveEvent
    {
        public ActiveEvent()
        {
        }

        [JsonConstructor]
        public ActiveEvent(int activeSeatNum,  long addCoins)
        {
            ActiveSeatNum = activeSeatNum;
            AddCoins = addCoins;
        }


        /// <summary>
        /// 激活玩家座位号
        /// </summary>
        public int ActiveSeatNum { get; private set; }

        /// <summary>
        /// 跟牌显示筹码数, 如果为0 表示过牌
        /// </summary>
        public long AddCoins { get; private set; }
    }

    public class DealThirdCardEvent
    {
        public DealThirdCardEvent()
        {
        }

        [JsonConstructor]
        public DealThirdCardEvent(List<long> coinPool, PokerCard card, List<int> order, int cardType, int point)
        {
            CoinPool = coinPool;
            Card = card;
            Order = order;
            CardType = cardType;
            Point = point;
        }

        /// <summary>
        /// 下注分堆， 每一堆多少jinbi
        /// </summary>
        public List<long> CoinPool { get; private set; }

        /// <summary>
        /// 第三张牌
        /// </summary>
        public PokerCard Card { get; private set; }

        /// <summary>
        /// 三张牌组成的类型
        /// </summary>
        public int CardType { get; private set; }

        /// <summary>
        /// 如果类型是点数牌， 点数
        /// </summary>
        public int Point { get; private set; }

        /// <summary>
        /// 发牌顺序
        /// </summary>
        public List<int> Order { get; private set; }
    }

    /// <summary>
    /// 玩家弃牌事件
    /// </summary>
    public class DropEvent
    {
        public DropEvent()
        {
        }

        [JsonConstructor]
        public DropEvent(int dropSeatNum)
        {
            DropSeatNum = dropSeatNum;
        }


        /// <summary>
        /// 激活玩家座位号
        /// </summary>
        public int DropSeatNum { get; private set; }
    }

    public class PassEvent
    {
        public PassEvent()
        {
        }

        [JsonConstructor]
        public PassEvent(int seatNum)
        {
            SeatNum = seatNum;
        }


        /// <summary>
        /// 激活玩家座位号
        /// </summary>
        public int SeatNum { get; private set; }
    }

    public class FollowEvent
    {
        public FollowEvent()
        {
        }

        [JsonConstructor]
        public FollowEvent(int seatNum, long followCoins, long carry)
        {
            SeatNum = seatNum;
            FollowCoins = followCoins;
            Carry = carry;
        }

        /// <summary>
        /// 激活玩家座位号
        /// </summary>
        public int SeatNum { get; private set; }
        public long FollowCoins { get; private set; }

        /// <summary>
        /// 当前携带为0表示allin
        /// </summary>
        public long Carry { get; private set; }
    }

    public class AddEvent
    {
        public AddEvent()
        {
        }

        [JsonConstructor]
        public AddEvent(int seatNum, long addCoins, long carry)
        {
            SeatNum = seatNum;
            AddCoins = addCoins;
            Carry = carry;
        }

        /// <summary>
        /// 激活玩家座位号
        /// </summary>
        public int SeatNum { get; private set; }
        public long AddCoins { get; private set; }

        /// <summary>
        /// 当前携带为0表示allin
        /// </summary>
        public long Carry { get; private set; }
    }

    /// <summary>
    /// 奖池
    /// </summary>
    public class WinnerCoinsPool
    {
        [JsonConstructor]
        public WinnerCoinsPool(int winnerSeat, long coins)
        {
            WinnerSeat = winnerSeat;
            Coins = coins;
        }

        /// <summary>
        /// 该奖池的赢家座位号
        /// </summary>
        public int WinnerSeat { get; private set; }

        /// <summary>
        /// 该奖池的金币数
        /// </summary>
        public long Coins { get; private set; }
    }

    
    public class PlayerCards
    {
        [JsonConstructor]
        public PlayerCards(List<PokerCard> cards, int cardType, int point)
        {
            Cards = cards;
            CardType = cardType;
            Point = point;
        }

        /// <summary>
        /// 手牌
        /// </summary>
        public List<PokerCard> Cards { get; private set; }

        /// <summary>
        /// 三张牌组成的类型
        /// </summary>
        public int CardType { get; private set; }

        /// <summary>
        /// 如果类型是点数牌， 点数
        /// </summary>
        public int Point { get; private set; }
    }

    /// <summary>
    /// 游戏结算
    /// </summary>
    public class GameOverEvent
    {
        [JsonConstructor]
        public GameOverEvent(List<int> winnerSeats, List<WinnerCoinsPool> winnerPool, Dictionary<int, PlayerCards> handCards)
        {
            WinnerSeats = winnerSeats;
            WinnerPool = winnerPool;
            this.handCards = handCards;
        }

        /// <summary>
        /// 赢家座位号
        /// </summary>
        public List<int> WinnerSeats { get; private set; }

        /// <summary>
        /// 每个奖池
        /// </summary>
        public List<WinnerCoinsPool> WinnerPool { get; private set; }

        /// <summary>
        /// 手牌
        /// </summary>
        public Dictionary<int, PlayerCards> handCards {get; private set;}
    }

    public class PlayerStanupEvent
    {
        [JsonConstructor]
        public PlayerStanupEvent(long userId, int seatNum)
        {
            UserId = userId;
            SeatNum = seatNum;
        }

        public long UserId { get; private set; }
        public int SeatNum { get; private set; }
    }

    public class ApplyStandupCommand
    {

    }


    public class ApplyLeaveCommand
    {

    }

    public class ApplySitdownCommand
    {
        [JsonConstructor]
        public ApplySitdownCommand(int seatNum)
        {
            SeatNum = seatNum;
        }

        public int SeatNum { get; private set; }

    }

    public class ApplyDropCommand
    {

    }

    public class ApplyPassCommand
    {

    }
    public class ApplyFollowCommand
    {

    }

    public class ApplyAddCommand
    {
        [JsonConstructor]
        public ApplyAddCommand(long addCoins)
        {
            this.AddCoins = addCoins;
        }

        public long AddCoins { get; private set; }
    }
}
