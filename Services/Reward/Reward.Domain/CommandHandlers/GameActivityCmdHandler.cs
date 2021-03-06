﻿using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using System.Linq;
using System.Collections.Generic;
using Commons.Extenssions.Defines;
using Commons.MqCommands;
using Commons.Extenssions;

namespace Reward.Domain.CommandHandlers
{
    public class GameActivityCmdHandler :
        IRequestHandler<GameActivityCommand, List<OneGameActivityInfoVM>>,
        IRequestHandler<GetGameActRewardCommand, BodyResponse<RewardInfoVM>>,
        IRequestHandler<AddActFromGamelogCommand, Unit>
    {
        protected readonly IMediatorHandler _bus;
        private readonly IActivityRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly AllGameActivityConfig _activityConfig;
        public GameActivityCmdHandler(IActivityRedisRepository redis,
            IMediatorHandler bus,
            AllGameActivityConfig regsterConfig, 
            IBusControl mqBus)
        {
            _redis = redis;
            _bus = bus;
            _activityConfig = regsterConfig;
            _mqBus = mqBus;
        }

        public async Task<List<OneGameActivityInfoVM>> Handle(GameActivityCommand request, 
            CancellationToken cancellationToken)
        {
            //通过配置获取今天所有的打牌活动activeId;
            DateTime tnow = DateTime.Now;
            List<Task<OneGameActivityInfo>> tasks = new List<Task<OneGameActivityInfo>>();
            foreach (var oneActivitty in _activityConfig.AllGameConfigs)
            {
                if (oneActivitty.ActivityType == request.Type)
                {
                    tasks.Add(_redis.GetGameActivity(tnow, request.Id, 
                        oneActivitty.ActivityId));
                   
                }
            }
            await Task.WhenAll(tasks);
            List<OneGameActivityInfo> playActivityInfos = tasks.Select(x => x.Result).ToList();
            List<OneGameActivityInfoVM> gameActivityVMs = new List<OneGameActivityInfoVM>();
            foreach (var one in playActivityInfos)
            {
                var oneConfig = _activityConfig.AllGameConfigs.Find(x => x.ActivityId == one.ActivityId);
                var roomList = new List<OneRoomActivityInfoVM>();
                foreach (var oneRoom in oneConfig.RoomConfigs)
                {
                    if (!one.CountProgress.TryGetValue(oneRoom.SubId, out var subAct))
                    {
                        subAct =  new GameSubActInfo(0, 0); 
                    }
                    OneRoomActivityInfoVM oneRoomInfo =
                        new OneRoomActivityInfoVM()
                        {
                            CurCount = subAct.CurCount,
                            State = subAct.State,
                            NeedCount = oneRoom.NeedCount,
                            RewardCoins = oneRoom.RewardCoins,
                            SubId = oneRoom.SubId,
                            Title = oneRoom.Title
                        };
                    roomList.Add(oneRoomInfo);
                }
                OneGameActivityInfoVM oneInfo = new OneGameActivityInfoVM(one.ActivityId, oneConfig.Title, roomList);
                gameActivityVMs.Add(oneInfo);

            }

            return gameActivityVMs;
        }

        public async Task<BodyResponse<RewardInfoVM>> Handle(GetGameActRewardCommand request, 
            CancellationToken cancellationToken)
        {
            DateTime tnow = DateTime.Now;
            var roomConfig = _activityConfig.AllGameConfigs
                .Find(x => x.ActivityId == request.ActId).RoomConfigs
                .Find(x => x.SubId == request.SubId);
            using (var locker = _redis.Locker(KeyGenHelper.GenUserDayKey(tnow, request.Id, "GameActivity", request.ActId)))
            {
                await locker.LockAsync();
                var subAct = await _redis.GetGameActProgress(tnow, request.Id, request.ActId, request.SubId);

                long rewardCoins = 0;
                if (subAct.State == 1)
                {
                    return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
                }
                if (subAct.CurCount >= roomConfig.NeedCount)
                {
                    rewardCoins = roomConfig.RewardCoins;
                    _ = _mqBus.Publish(new AddMoneyMqCommand(request.Id, rewardCoins, 0, AddReason.GameAct));
                    subAct.State = 1;
                    await _redis.SetGameActProgress(tnow, request.Id, request.ActId, request.SubId, subAct);
                    return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success,
                        null, new RewardInfoVM(rewardCoins));
                }
                else
                {
                    return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Error);
                }
            }
            
        }

        public void DealOneRoom(DateTime time, List<long> players, string actId, string subId)
        {
            ParallelLoopResult result = Parallel.ForEach(players, async player =>
            {
                using (var locker = _redis.Locker(KeyGenHelper.GenUserDayKey(time, player, "GameActivity", actId)))
                {
                    await locker.LockAsync();
                    var oneSub = await _redis.GetGameActProgress(time, player, actId, subId);
                    if (oneSub == null)
                    {
                        oneSub = new GameSubActInfo(0, 0);
                    }
                    if (oneSub.State == 1)
                    {
                        return;
                    }
                    ++oneSub.CurCount;
                    await _redis.SetGameActProgress(time, player, actId, subId, oneSub);
                }               
            });
        }

        public Task<Unit> Handle(AddActFromGamelogCommand request, CancellationToken cancellationToken)
        {
            DateTime tnow = DateTime.Now;
            foreach( var oneAct in _activityConfig.AllGameConfigs)
            {
                List<long> players = null;
                if (oneAct.ActivityType == ActivityTypes.WinGame)
                {
                    players = request._allPlayers.Where(x => x.Value > 0).Select(x => x.Key).ToList();
                }
                else
                {
                    players = request._allPlayers.Select(x => x.Key).ToList();
                }
                foreach (var oneRoom in oneAct.RoomConfigs)
                {
                    if (oneRoom.RoomType == request.RoomType)
                    {
                        DealOneRoom(tnow, players, oneAct.ActivityId, oneRoom.SubId);
                    }
                }
               
            }
            return Task.FromResult(new Unit());
        }
    }
}
