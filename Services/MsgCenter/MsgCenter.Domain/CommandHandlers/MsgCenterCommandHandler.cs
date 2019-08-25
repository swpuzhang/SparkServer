using MsgCenter.Domain.Commands;
using MsgCenter.Domain.Models;
using MsgCenter.Domain.RepositoryInterface;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using Commons.Extenssions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using MassTransit;
using Commons.MqCommands;

namespace MsgCenter.Domain.CommandHandlers
{
    public class MsgCenterCommandHandler :
        IRequestHandler<GetUserMsgsCommand, BodyResponse<UserMsgs>>,
        IRequestHandler<ReadedCommand, BodyResponse<NullBody>>,
        IRequestHandler<RecieveMsgReward, BodyResponse<NullBody>>,
        IRequestHandler<DeleteMsgCommand, BodyResponse<NullBody>>,
        IRequestHandler<ReadedAllCommand, BodyResponse<NullBody>>,
        IRequestHandler<RecieveAllMsgRewardCommand, BodyResponse<List<RewardInfo>>>,
        IRequestHandler<PushMsgCommand, BodyResponse<NullBody>>
        





    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
       
        private readonly IMsgCenterRedisRepository _redis;
        private readonly IBusControl _mqBus;
        public MsgCenterCommandHandler( 
            IMsgCenterRedisRepository redis, IMediatorHandler bus, IBusControl mqBus)
        {
            _redis = redis;
            _bus = bus;
            _mqBus = mqBus;
        }
        public async Task<BodyResponse<UserMsgs>> Handle(GetUserMsgsCommand request, CancellationToken cancellationToken)
        {
            var  allMsgs = await _redis.GetAllMsg(request.Id, request.MsgType);
            if (allMsgs == null)
            {
                return new BodyResponse<UserMsgs>(StatusCodeDefines.Success);
            }
           return new BodyResponse<UserMsgs>(StatusCodeDefines.Success, 
                null, new UserMsgs(allMsgs.Values.ToList()));
        }

        public async Task<BodyResponse<NullBody>> Handle(ReadedCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", request.MsgType.ToString())))
            {
                await locker.LockAsync();
                await _redis.ReadedMsg(request.Id, request.MsgType, request.MsgId);
            }
            return new BodyResponse<NullBody>(StatusCodeDefines.Success);
        }

        public async Task<BodyResponse<NullBody>> Handle(RecieveMsgReward request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", MsgTypes.Reward.ToString())))
            {
                await locker.LockAsync();
                var msg = await _redis.GetMsgInfo(request.Id, MsgTypes.Reward, request.MsgId);
                if (msg != null && msg.MsgState != MsgStates.Recieved)
                {
                    List<Task> tasks = new List<Task>();
                    foreach (var one in msg.RewardInfo)
                    {
                        var rewardMethod = one.GenRewardMethod(one.RewardType);
                        if (rewardMethod == null)
                        {
                            continue;
                        }
                        tasks.Add(rewardMethod.AddReward(_mqBus, request.Id, one));
                    }
                    await Task.WhenAll(tasks);
                    msg.MsgState = MsgStates.Recieved;
                    await _redis.SetMsgInfo(request.Id, MsgTypes.Reward, msg);
                }
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);   
            }
        }

        public async Task<BodyResponse<NullBody>> Handle(DeleteMsgCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", MsgTypes.Reward.ToString())))
            {
                await _redis.DeleteMsg(request.Id, request.MsgType, request.MsgId);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }

        public async Task<BodyResponse<NullBody>> Handle(ReadedAllCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", request.MsgType.ToString())))
            {
                await locker.LockAsync();
                await _redis.ReadedAllMsg(request.Id, request.MsgType);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }

        public async Task<BodyResponse<List<RewardInfo>>> Handle(RecieveAllMsgRewardCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", MsgTypes.Reward.ToString())))
            {
                await locker.LockAsync();
                var msgs = await _redis.GetAllMsg(request.Id, MsgTypes.Reward);
                List<Task> tasks = new List<Task>();
                List<RewardInfo> allreward = new List<RewardInfo>();
                foreach (var one in msgs)
                {
                    if (one.Value.MsgState != MsgStates.Recieved)
                    {
                        foreach (var oneRward in one.Value.RewardInfo)
                        {
                            var rewardMethod = oneRward.GenRewardMethod(oneRward.RewardType);
                            if (rewardMethod == null)
                            {
                                continue;
                            }
                            allreward.Add(oneRward);
                            tasks.Add(rewardMethod.AddReward(_mqBus, request.Id, oneRward));
                        }
                    }
                    one.Value.MsgState = MsgStates.Recieved;
                    tasks.Add(_redis.SetMsgInfo(request.Id, MsgTypes.Reward, one.Value));
                }
                await Task.WhenAll(tasks);
                return new BodyResponse<List<RewardInfo>>(StatusCodeDefines.Success, null, allreward);
            }
        }

        public async Task<BodyResponse<NullBody>> Handle(PushMsgCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "Msgs", MsgTypes.Reward.ToString())))
            {
                await locker.LockAsync();
                await _redis.AddMsgInfo(request.Id, request.Msg.MsgType, request.Msg);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }
    }
}
