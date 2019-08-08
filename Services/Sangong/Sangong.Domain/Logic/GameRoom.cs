﻿using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Sangong.Domain.Manager;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Commons.IntegrationBus;
using Commons.Domain.Managers;
using Sangong.Domain.Models;
using MassTransit;
using Sangong.GameMessage;
using AutoMapper;
using Commons.MqCommands;
using Sangong.MqEvents;

namespace Sangong.Domain.Logic
{
    
    public class GameRoom
    {
        private List<GameSeat> _seats = new List<GameSeat>();
        private Dictionary<long, GamePlayer> _playerInfos = new Dictionary<long, GamePlayer>();
        private MqManager _mqManager;
        private readonly IBusControl _bus;
        private IMapper _mapper;
        private List<PokerCard> _bottomCards;
        #region 成员
        public string RoomId { get; private set; }
        public long Blind { get; private set; }
        public long MinCoins { get; private set; }
        public long MaxCoins { get; private set; }
        public int TipsPersent { get; private set; }

        public long MinCarry { get; private set; }

        public long MaxCarry { get; private set; }

        public int SeatCount { get; private set; }

        public int ActiveSeatNum { get; private set; } = -1;

        public GameStatusLogic _statusInfo = new GameStatusLogic();

        private int _dealerSeatIndex = 0;

        private int _secondDealer = 0;

        private long _maxAdd = 0;

        private int lastWinSeat = -1;

        private CoinsPool _coinsPool = new CoinsPool();

        public void Clean()
        {
            ActiveSeatNum = -1;
            _dealerSeatIndex = -1;
            _secondDealer = -1;
            _maxAdd = 0;
            _coinsPool.Clean();
            _bottomCards.Clear();
            foreach (var seat in _seats)
            {
                seat.Clean();
            }

        }

        #endregion

        #region 初始化
        public GameRoom(
           string roomId,
           long blind,
           int seatCount,
           long minCoins,
           long maxCoins,
           int tipsPersent,
           MqManager mqManager,
           IBusControl bus, IMapper mapper, long minCarry, long maxCarry)
        {
            RoomId = roomId;
            Blind = blind;
            MinCoins = minCoins;
            MaxCoins = maxCoins;
            TipsPersent = tipsPersent;
            SeatCount = seatCount;
            _mqManager = mqManager;
            _bus = bus;
            _mapper = mapper;
            MinCarry = minCarry;
            MaxCarry = maxCarry;
        }
        public void Init()
        {
            for (int i = 0; i < SeatCount; ++i)
            {
                _seats.Add(new GameSeat(i));
            }
        }

        public void EnsurDealer()
        {
            if (lastWinSeat == -1 || !_seats[lastWinSeat].IsSeated())
            {
                _dealerSeatIndex = _seats.Where(x => x.IsSeated()).First().SeatNum;
            }
            _dealerSeatIndex = lastWinSeat;
        }

        #endregion
        public int NextSeatedNum(int curNum)
        {
            int index = (curNum + 1) % _seats.Count;
            while (!_seats[index].IsSeated() && index != curNum)
            {
                index = (index + 1) % _seats.Count;
            }
            return index;
        }
        public int NextInGameNum(int curNum)
        {
            int index = (curNum + 1) % _seats.Count;
            while (!_seats[index].IsCanContinue() && index != curNum)
            {
                index = (index + 1) % _seats.Count;
            }
            return index;
        }

        public bool NextActiveNum()
        {
            if (ActiveSeatNum == -1)
            {
                ActiveSeatNum = _dealerSeatIndex;
                if (_seats[ActiveSeatNum].IsCanActive(_maxAdd))
                {
                    return true;
                }
            }
            int index = -1;
            index = (ActiveSeatNum + 1) % _seats.Count;
            //对于已经allin 或者加注过的跳过
            while (!_seats[index].IsCanActive(_maxAdd))
            {
                index = (index + 1) % _seats.Count;
                if (index == ActiveSeatNum)
                {
                    break;
                }
            }
            if (index == ActiveSeatNum)
            {
                return false;
            }
            ActiveSeatNum = index;
            return true;
        }
        public bool IsGameCanStart()
        {
            if (_statusInfo.IsGameCanStart() && GetPlayerCount() > 1)
            {
                return true;
            }

            return false;
        }
        public int GetPlayerCount()
        {
            int count = 0;
            foreach (var seat in _seats)
            {
                if (seat.IsSeated())
                {
                    ++count;
                }
            }
            return count;
        }

        public int GetInGameCount()
        {
            int count = 0;
            foreach (var seat in _seats)
            {
                if (seat.IsCanContinue())
                {
                    ++count;
                }
            }
            return count;
        }

        public GameSeat GetEmptySeat()
        {
            return _seats.Where(x => !x.IsSeated()).FirstOrDefault();
        }
        public async Task<BodyResponse<JoinGameRoomMqResponse>> JoinRoom(long id)
        {
            _playerInfos.TryGetValue(id, out var player);
            //已经在房间直接返回成功
            if (player != null)
            {
                return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Success, null,
                    new JoinGameRoomMqResponse(id, RoomId, GameRoomManager.gameKey, GetPlayerCount(), Blind));
            }

            //查找空位
            var seat = GetEmptySeat();
            if (seat == null)
            {
                return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Error, new List<string>() { "room is full " }, null);
            }
            
            player = await MakePlayer(id);
            if (player == null)
            {
                return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Error, new List<string>() { "make player error " }, null);
            }

            //注意异步回来后, 可能整个对象都变化了, 所以要重新判断
            if (!player.IsSeated())
            {
                seat = GetEmptySeat();
                if (seat == null)
                {
                    return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Error, new List<string>() { "room is full " }, null);
                }
                player.Seat(seat);
                BroadCastMessage(new PlayerSeatedEvent(player.Id, player.UserName,
                    player.Coins, player.Diamonds, player.SeatInfo.SeatNum, player.Carry), 
                    "PlayerSeatedEvent");
                //判断房间人数>2 人， 而且是正在准备状态， 开始牌局， 否者旁观
                if (IsGameCanStart())
                {
                    _statusInfo.WaitForNexStatus(OnGameReady, GameStatus.ready, GameTimerConfig.ReadyWait);
                }
            }
            
            return new BodyResponse<JoinGameRoomMqResponse>(StatuCodeDefines.Success, null,
                new JoinGameRoomMqResponse(id, RoomId, GameRoomManager.gameKey, GetPlayerCount(), Blind));
        }
        public void BroadCastMessage(object request, string reqName)
        {
            foreach (var player in _playerInfos)
            {
                GameServerRequest GameRequest = new GameServerRequest(player.Key, request,
                    reqName, GameRoomManager.gameKey, RoomId);
                _bus.Publish<GameServerRequest>(GameRequest);
            }
        }
        public void BroadCastMessage(object request, string reqName,  GamePlayer player)
        {
            GameServerRequest GameRequest = new GameServerRequest(player.Id, request, reqName,
                    GameRoomManager.gameKey, RoomId);
                _bus.Publish<GameServerRequest>(GameRequest);
        }

        #region 超时处理
        public void OnGameIdle()
        {
            if (IsGameCanStart())
            {
                _statusInfo.WaitForNexStatus(OnGameReady, GameStatus.ready, GameTimerConfig.ReadyWait);
            }
        }

        public void OnGameReady()
        {
            if (IsGameCanStart())
            {
                _statusInfo.WaitForNexStatus(OnGamePlaying, GameStatus.playing, 0);
            }
            else
            {
                _statusInfo.WaitForNexStatus(OnGameIdle, GameStatus.Idle, 0);
            }
        }
        public void OnGamePlaying()
        {
            Clean();
            //定庄
            EnsurDealer();

            //发牌
            CardDealer.DealCard(GetPlayerCount(), out var allUserCards, out _bottomCards);

            //从庄家开始发牌,下注
            int index = _dealerSeatIndex;
            List<int> dealerOrder = new List<int>();
            List<long> carrys = new List<long>();
            do
            {
                dealerOrder.Add(index);
                _seats[index].DealCard(allUserCards.Last(), Blind);
                carrys.Add(_seats[index].InGamePlayerInfo.Carry);
                allUserCards.RemoveAt(allUserCards.Count - 1);
            } while ((index = NextSeatedNum(index)) != _dealerSeatIndex);

            foreach (var player in _playerInfos)
            {
                DealCardsEvent dealCard = new DealCardsEvent(_dealerSeatIndex, dealerOrder,
                    CardDealer.UserCardsCount,
                    player.Value.SeatInfo?.handCards, Blind, carrys);
                BroadCastMessage(dealCard, "DealCardsEvent", player.Value);
            }
            //等待发牌结束
            _statusInfo.WaitForNexStatus(OnDealingCards, GameStatus.playing, GameTimerConfig.DealCard);
        }
        public void OnDealingCards()
        {

            ActivePlayer(true);
        }

        public void OnDealingThirdCard()
        {
            ActivePlayer(false);
        }

        public async void OnGameOver()
        {
            List<Task<MoneyMqResponse>> tasks = new List<Task<MoneyMqResponse>>();
            List<KeyValuePair<GameSeat, Task<MoneyMqResponse>>> seatTasks = new List<KeyValuePair<GameSeat, Task<MoneyMqResponse>>>();
            //检查玩家携带是否足够
            foreach (var seat in _seats)
            {
                if (!seat.IsSeated())
                {
                    continue;
                }
                var player = seat.PlayerInfo;
                if (player.Carry >=  MinCarry)
                {
                    continue;
                }
                var moneyInfo = _mqManager.BuyIn(player.Id, MinCarry, MaxCarry);
                tasks.Add(moneyInfo);
                seatTasks.Add(new KeyValuePair<GameSeat, Task<MoneyMqResponse>>(seat, moneyInfo));
            }
            await Task.WhenAll(tasks);
            foreach (var oneResult in seatTasks)
            {
                if (!oneResult.Key.IsSeated() || oneResult.Key.PlayerInfo.Carry >= MinCarry 
                    || oneResult.Key.PlayerInfo.Id != oneResult.Value.Result.Id)
                {
                    continue;
                }
                if (oneResult.Value.Result == null)
                {
                    //买入失败
                    PlayerStandup(oneResult.Key.PlayerInfo);
                }
                else
                {
                    oneResult.Key.PlayerInfo.BuyIn(oneResult.Value.Result.CurCoins, oneResult.Value.Result.CurDiamonds,
                        oneResult.Value.Result.Carry);
                }
            }
            _statusInfo.WaitForNexStatus(OnGameIdle, GameStatus.Idle, 1000);
        }

        public void OnPlayerOpt()
        {
            GameSeat seat = _seats[ActiveSeatNum];
            //超时, 默认过牌,否则弃牌,然后激活下一个玩家
            if (seat.BetedCoins < _maxAdd)
            {
                PlayerDrop(seat.PlayerInfo);
            }
            else
            {
                PlayerPass(seat.PlayerInfo);
            }
        }
        #endregion


        public void PlayerDrop(GamePlayer player)
        {
            BroadCastMessage(new DropEvent(player.SeatInfo.SeatNum), "DropEvent");
            player.SeatInfo.Drop();
            if (GetInGameCount() < 2)
            {
                GameAccount();
            }
            else
            {
                
                ActivePlayer(_statusInfo.IsFirstRound());
            }
            
        }

        public void PlayerPass(GamePlayer player)
        {
            BroadCastMessage(new PassEvent(player.SeatInfo.SeatNum), "PassEvent");
            player.SeatInfo.Follow(0);
            ActivePlayer(_statusInfo.IsFirstRound());
        }

        public bool PlayerFollow(GamePlayer player, out long followChips)
        {
            followChips = _maxAdd - player.SeatInfo.BetedCoins;
            followChips = followChips <= player.Carry ? followChips : player.Carry;
            if (player.Carry == 0 || player.SeatInfo.BetedCoins >= _maxAdd 
                    || !player.SeatInfo.Follow(followChips))
            {
                return false;
            }
            BroadCastMessage(new FollowEvent(player.SeatInfo.SeatNum, followChips, player.Carry), "FollowEvent");
            ActivePlayer(_statusInfo.IsFirstRound());
            return true;
        }

        public bool PlayerAdd(GamePlayer player, long addChips)
        {
            if (player.Carry < addChips || player.SeatInfo.BetedCoins > _maxAdd
                || !player.SeatInfo.Add(addChips))
            {
                return false;
            }
            BroadCastMessage(new AddEvent(player.SeatInfo.SeatNum, addChips, player.Carry), "AddEvent");
            ActivePlayer(_statusInfo.IsFirstRound());
            return true;
        }

        public async Task<GamePlayer> MakePlayer(long id)
        {
            var accountInfo =   _mqManager.GetAccountInfo(id);
            var moneyInfo =  _mqManager.BuyIn(id, MinCarry, MaxCarry);
            await Task.WhenAll(accountInfo, moneyInfo);
            if (moneyInfo == null)
            {
                return null;
            }
            GamePlayer player = null;
            if (accountInfo == null)
            {
                player = new GamePlayer(id, moneyInfo.Result.CurCoins, moneyInfo.Result.CurDiamonds, moneyInfo.Result.Carry);
            }
            else
            {
                player = new GamePlayer(id, accountInfo.Result.PlatformAccount, accountInfo.Result.UserName, accountInfo.Result.Sex,
                    accountInfo.Result.HeadUrl, moneyInfo.Result.CurCoins, moneyInfo.Result.CurDiamonds, moneyInfo.Result.Carry);
            }
            if (_playerInfos.TryGetValue(id, out var existPlayer))
            {
                return existPlayer;
            }
            _playerInfos.Add(id, player);
            return player;
        }
        public long FollowCoins(GameSeat seat)
        {
            long followChips = 0;
            if (seat.BetedCoins < _maxAdd)
            {
                if (seat.PlayerInfo.Carry >= _maxAdd)
                {
                    followChips = _maxAdd;
                }
                else
                {
                    followChips = seat.PlayerInfo.Carry;
                }

            }
            return followChips;

        }
        public void ActivePlayer(bool isFirstRound)
        {
            
            if (!NextActiveNum())
            {
                if (isFirstRound)
                {
                    StartSecondRound();
                }
                else
                {
                    //比牌结算
                    GameAccount();
                }
            }
            else
            {
                var seat = _seats[ActiveSeatNum];
                ActiveEvent actEvent = new ActiveEvent(ActiveSeatNum, FollowCoins(seat));
                BroadCastMessage(actEvent, "ActiveEvent");
                _statusInfo.WaitForNexStatus(OnPlayerOpt, GameStatus.FirstRound, GameTimerConfig.BetChips);
            }
        }

        public void FindSecondRondDealer()
        {
            int index = _dealerSeatIndex;
            while (!_seats[index].IsCanContinue())
            {
                index = (index + 1) % _seats.Count;
                if (index == _dealerSeatIndex)
                {
                    break;
                }
            }
            _secondDealer = index;
        }

        public void StartSecondRound()
        {
            //重置下下注, 激活玩家
            _maxAdd = 0;
            ActiveSeatNum = -1;
            List<int> order = new List<int>();
            FindSecondRondDealer();
            int index = _secondDealer; ;
            do
            {
                _seats[index].SecondRoundStarted(_bottomCards.Last());
                _bottomCards.RemoveAt(_bottomCards.Count - 1);
                order.Add(index);
            } while ((index = NextInGameNum(index)) != _secondDealer);
            
            foreach (var one in _playerInfos)
            {
                DealThirdCardEvent thirdEvent = null;
                if (!one.Value.IsSeated())
                {
                    thirdEvent = new DealThirdCardEvent(_coinsPool.GetFirstPoolsResult(),
                        null, order, 0, 0);
                    
                }
                else
                {
                    if (one.Value.SeatInfo.IsCanContinue())
                    {
                        thirdEvent = new DealThirdCardEvent(_coinsPool.GetFirstPoolsResult(),
                        one.Value.SeatInfo.handCards.Last(), order, (int)one.Value.SeatInfo.Combination.ComType,
                        one.Value.SeatInfo.Combination.Point);
                    }
                    else
                    {
                        thirdEvent = new DealThirdCardEvent(_coinsPool.GetFirstPoolsResult(),
                        null, order, 0, 0);
                    }
                }
                BroadCastMessage(thirdEvent, "DealThirdCardEvent", one.Value);
            }

            _statusInfo.WaitForNexStatus(OnDealingThirdCard, GameStatus.playing, GameTimerConfig.DealThirdCard);
        }

        public void PlayerStandup(GamePlayer player)
        {
            PlayerStanupEvent standupEvent = new PlayerStanupEvent(player.Id, player.SeatInfo.SeatNum);
            BroadCastMessage(standupEvent, "PlayerStanupEvent");
            player.Standup();

            //返还携带
            _bus.Publish(new AddMoneyMqCommand(player.Id, player.Carry, -player.Carry));

            //告诉matchingserver该玩家已经站起
            _bus.Publish(new LeaveGameRoomMqEvent(player.Id, RoomId, GameRoomManager.gameKey,
                GetPlayerCount(), Blind, GameRoomManager.matchingGroup));

            if (GetInGameCount() < 2)
            {
                GameAccount();
            }
            else
            {
                if (ActiveSeatNum == player.SeatInfo.SeatNum)
                {
                    ActivePlayer(_statusInfo.IsFirstRound());
                }
            }
            //返还携带
           
            
        }

        public void GameAccount()
        {
            GameOverEvent gameOverEvent = GameAccounter.Caculate(_seats, _coinsPool.GetSecondPools());
            BroadCastMessage(gameOverEvent, "GameOverEvent");
            foreach (var seat in _seats)
            {
                if (seat.InGamePlayerInfo == null)
                {
                    continue;
                }

                if (seat.WinCoins > 0)
                {
                    

                    //说明这个赢的玩家已经走了, 那么将钱还是加给他
                    if (seat.InGamePlayerInfo != seat.PlayerInfo)
                    {
                        _bus.Publish(new AddMoneyMqCommand(seat.InGamePlayerInfo.Id, seat.WinCoins, 0));
                    }
                    else
                    {
                        seat.PlayerInfo.AddCarry(seat.WinCoins);
                        _bus.Publish(new AddMoneyMqCommand(seat.InGamePlayerInfo.Id, 0, seat.WinCoins));
                    }
                }
                else
                {
                    _bus.Publish(new AddMoneyMqCommand(seat.InGamePlayerInfo.Id, 0, -seat.TotalBetedCoins));
                }
            }
            _statusInfo.WaitForNexStatus(OnGameOver, GameStatus.Idle, GameTimerConfig.GameAccount);
            Clean();
        }

        public bool IsPlayerActive(GamePlayer player)
        {
            if (player.IsSeated() && player.SeatInfo.SeatNum == ActiveSeatNum)
            {
                return true;
            }
            return false;
        }

        public void PlayerLeave(GamePlayer player)
        {
            if (player.IsSeated())
            {
                player.Standup();
            }
            _playerInfos.Remove(player.Id);
        }

        #region 消息处理
        public CommonResponse OnApplyStandupCommand(long id, Guid gid, ApplyStandupCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null || !player.IsSeated())
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }
            PlayerStandup(player);
            return new CommonResponse(gid);
        }

        public CommonResponse OnApplyLeaveCommand(long id, Guid gid, ApplyLeaveCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null)
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }
            PlayerStandup(player);
            return new CommonResponse(gid);
        }

        public CommonResponse OnApplyDropCommand(long id, Guid gid, ApplyDropCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null || !IsPlayerActive(player))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }
            PlayerDrop(player);
            return new CommonResponse(gid);
        }

        public CommonResponse OnApplyPassCommand(long id, Guid gid, ApplyPassCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null || !IsPlayerActive(player))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }
            
            PlayerPass(player);
            return new CommonResponse(gid);
        }


        public CommonResponse OnApplyFollowCommand(long id, Guid gid, ApplyFollowCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null || !IsPlayerActive(player))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }

            if (!PlayerFollow(player, out var followChips))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.NoEnoughMoney, null);
            }
            
            return new CommonResponse(gid);
        }

        public CommonResponse OnApplyAddCommand(long id, Guid gid, ApplyAddCommand command)
        {
            _playerInfos.TryGetValue(id, out var player);
            if (player == null || !IsPlayerActive(player))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.PlayerNotInRoom, null);
            }

            if (!PlayerAdd(player, command.AddCoins))
            {
                return new CommonResponse(null, gid, StatuCodeDefines.NoEnoughMoney, null);
            }

            return new CommonResponse(gid);
        }
        #endregion
    }
}
