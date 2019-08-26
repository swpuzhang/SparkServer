using Friend.Domain.Commands;
using Friend.Domain.Models;
using Friend.Domain.RepositoryInterface;
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
using Commons.MqEvents;

namespace Friend.Domain.CommandHandlers
{
    public class FriendCommandHandler :
        IRequestHandler<ApplyAddFriendCommand, BodyResponse<NullBody>>,
        IRequestHandler<AgreeAddFriendCommand, BodyResponse<NullBody>>,
        IRequestHandler<GetFriendsCommand, BodyResponse<FriendVM>>,
        IRequestHandler<GetApplysCommand, BodyResponse<FriendVM>>,
        IRequestHandler<IgnoreApplyCommand, BodyResponse<NullBody>>,
        IRequestHandler<DeleteFriendCommand, BodyResponse<NullBody>>,
        IRequestHandler<UploadPlatformFriendsCommand, BodyResponse<NullBody>>,
        IRequestHandler<GetFriendInfoCommand, BodyResponse<GetFriendInfoMqResponse>>
        


    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IFriendInfoRepository _friendRepository;
        private readonly IFriendRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IRequestClient<GetAccountBaseInfoMqCommand> _mqCleint;
        private readonly IRequestClient<GetIdByPlatformMqCommand> _mqGetIdCleint;
        public FriendCommandHandler(IFriendInfoRepository rep, IFriendRedisRepository redis,
            IMediatorHandler bus, IBusControl mqBus, IRequestClient<GetAccountBaseInfoMqCommand> mqCleint, 
            IRequestClient<GetIdByPlatformMqCommand> mqGetIdCleint)
        {
            _friendRepository = rep;
            _redis = redis;
            _bus = bus;
            _mqBus = mqBus;
            _mqCleint = mqCleint;
            _mqGetIdCleint = mqGetIdCleint;
        }


        public async Task<BodyResponse<NullBody>> Handle(ApplyAddFriendCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            using(var lockerFriend = _redis.Locker(KeyGenHelper.GenUserKey(request.FriendId, "FriendInfo")))
            {
                await Task.WhenAll(locker.LockAsync(), lockerFriend.LockAsync());
                var infos = await LoadToRedis(request.Id, request.FriendId);
                var selfInfo = infos[0];
                var friendInfo = infos[1];
                bool isFriend = friendInfo._friends.ContainsKey(request.FriendId);
                if (isFriend)
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.IsAlreadyFriend);
                }
                bool isApplyed = await _redis.IsAlreadyApplyed(request.FriendId, request.Id);
                if (isApplyed)
                {
                    bool isApplyedEachOther = await _redis.IsAlreadyApplyed(request.Id, request.FriendId);
                    if (isApplyedEachOther)
                    {
                        await AddFriend(request.FriendId, request.Id, 0);
                        return new BodyResponse<NullBody>(StatusCodeDefines.Success);
                    }
                    return new BodyResponse<NullBody>(StatusCodeDefines.IsAlreadyApplyed);
                }

                List<Task<long>> tFrindCounts = new List<Task<long>>
                {
                    _redis.GetApplyedFriendCount(request.FriendId),
                    _redis.GetFriendCount(request.FriendId),
                    _redis.GetFriendCount(request.Id)
                };
                await Task.WhenAll(tFrindCounts);
                if (tFrindCounts.Where(x =>x.Result >= 1000).Count() > 0)
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.IsFull);
                }
                
                await _redis.AddApplyedFriend(request.FriendId, request.Id);
                _mqBus.PublishServerReqExt(request.FriendId, new ApplyedAddFriendMqEvent(request.Id));
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }

        public async Task<FriendInfo> LoadFriends(long id)
        {
           
            var info = await _redis.GetFriendInfo(id);
            if (info == null)
            {
                info = await _friendRepository.GetByIdAsync(id);
            }

            await _redis.SetFriendInfo(info);

            return info;
        }

        public async Task LoadToRedis(long id)
        {
            if (!await _redis.IsFriendInRedis(id))
            {
                var info = await _friendRepository.GetByIdAsync(id);
                await _redis.SetFriendInfo(info);
            }
        }

        public async Task<FriendInfo[]> LoadToRedis(long id, long friendId)
        {
            FriendInfo selfInfo = null;
            FriendInfo friendInfo = null;
            var t1 = _redis.GetFriendInfo(id);
            var t2 = _redis.GetFriendInfo(friendId);
            await Task.WhenAll(t1, t2);
           
            Task<FriendInfo>[] tinfos = new Task<FriendInfo>[2];
            if (t1.Result == null)
            {
                tinfos[0] = _friendRepository.GetByIdAsync(id);
            }
            else
            {
                selfInfo = t1.Result;
            }
            if (t2.Result == null)
            {
                tinfos[1] = _friendRepository.GetByIdAsync(friendId);
            }
            else
            {
                friendInfo = t2.Result;
            }
            await Task.WhenAll(tinfos);
            Task[] trets = new Task[2];
            if (tinfos[0] != null)
            {
                trets[0] = _redis.SetFriendInfo(tinfos[0].Result);
                selfInfo = tinfos[0].Result;
            }
            if (tinfos[1] != null)
            {
                trets[1] = _redis.SetFriendInfo(tinfos[1].Result);
                friendInfo = tinfos[1].Result;
            }
            await Task.WhenAll(trets);
            return new FriendInfo[] { selfInfo, friendInfo };
        }

        public async Task<BodyResponse<NullBody>> Handle(AgreeAddFriendCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            using (var lockerFriend = _redis.Locker(KeyGenHelper.GenUserKey(request.FriendId, "FriendInfo")))
            {
                await Task.WhenAll(locker.LockAsync(), lockerFriend.LockAsync());
                var infos =  await LoadToRedis(request.Id, request.FriendId);

                bool isFriend = false;
                if (infos[0] != null && infos[0]._friends.ContainsKey(request.FriendId))
                {
                    isFriend = true;
                }
                if (isFriend)
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.IsAlreadyFriend);
                }
                bool isApplyed = await _redis.IsAlreadyApplyed(request.FriendId, request.Id);
                if (!isApplyed)
                {
                    return new BodyResponse<NullBody>(StatusCodeDefines.IsNotApplyed);
                }
                await AddFriend(request.Id, request.FriendId, 0);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }

        public async Task AddFriend(long id, long friendId, FriendTypes type)
        {
            await LoadToRedis(id);
            await LoadToRedis(friendId);
            await Task.WhenAll(_redis.AddFriend(id, friendId, type),
            _friendRepository.AddFriend(id, friendId, type));
        }

        public async Task<BodyResponse<FriendVM>> Handle(GetFriendsCommand request, CancellationToken cancellationToken)
        {
            var info = await _redis.GetFriendInfo(request.Id);
            if (info == null)
            {
                info = await _friendRepository.GetByIdAsync(request.Id);
                using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
                {
                    await _redis.SetFriendInfo(info);
                }
   
            }
            if (info == null)
            {
                return new BodyResponse<FriendVM>(StatusCodeDefines.Success);
            }

            List<Task<Response<BodyResponse<GetAccountBaseInfoMqResponse>>>> getTasks =
                new List<Task<Response<BodyResponse<GetAccountBaseInfoMqResponse>>>>();
            foreach (var oneFriend in info._friends)
            {
                //获取好友信息
                getTasks.Add(_mqCleint.GetResponseExt<GetAccountBaseInfoMqCommand,BodyResponse<GetAccountBaseInfoMqResponse>>
                    (new GetAccountBaseInfoMqCommand(oneFriend.Key)));
            }
            var allTask = await Task.WhenAll(getTasks);
            FriendVM friendInfos = new FriendVM() { Friends = new List<OneFriendVM>() };
            foreach (var one in allTask)
            {
                var response = one.Message;
                if (response.StatusCode == StatusCodeDefines.Success)
                {
                    var friendType = info._friends[response.Body.Id].Type;
                    friendInfos.Friends.Add(new OneFriendVM(response.Body.Id, response.Body.PlatformAccount, response.Body.UserName,
                        response.Body.Sex, response.Body.HeadUrl, response.Body.Type, friendType));
                }
            }
           
            return new BodyResponse<FriendVM>(StatusCodeDefines.Success, null, friendInfos);
        }

        public async Task<BodyResponse<FriendVM>> Handle(GetApplysCommand request, CancellationToken cancellationToken)
        {
            var allApplys = await _redis.GetApplyInfo(request.Id);
            if (allApplys == null)
            {
                return new BodyResponse<FriendVM>(StatusCodeDefines.Success);
            }
            List<Task<Response<BodyResponse<GetAccountBaseInfoMqResponse>>>> getTasks =
               new List<Task<Response<BodyResponse<GetAccountBaseInfoMqResponse>>>>();
            foreach (var oneFriend in allApplys)
            {
                //获取好友信息
                getTasks.Add(_mqCleint.GetResponseExt<GetAccountBaseInfoMqCommand, BodyResponse<GetAccountBaseInfoMqResponse>>
                    (new GetAccountBaseInfoMqCommand(oneFriend)));
            }
            var allTask = await Task.WhenAll(getTasks);
            FriendVM friendInfos = new FriendVM() { Friends = new List<OneFriendVM>() };
            foreach (var one in allTask)
            {
                var response = one.Message;
                if (response.StatusCode == StatusCodeDefines.Success)
                {
                    
                    friendInfos.Friends.Add(new OneFriendVM(response.Body.Id, response.Body.PlatformAccount, response.Body.UserName,
                        response.Body.Sex, response.Body.HeadUrl, response.Body.Type, 0));
                }
            }
            return new BodyResponse<FriendVM>(StatusCodeDefines.Success, null, friendInfos);
        }

        public async Task<BodyResponse<NullBody>> Handle(IgnoreApplyCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            using (var lockerFriend = _redis.Locker(KeyGenHelper.GenUserKey(request.FriendId, "FriendInfo")))
            {
                await _redis.IgnoreApply(request.Id, request.FriendId);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
                
        }

        public async Task<BodyResponse<NullBody>> Handle(DeleteFriendCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            using (var lockerFriend = _redis.Locker(KeyGenHelper.GenUserKey(request.FriendId, "FriendInfo")))
            {
                await Task.WhenAll(_redis.DeleteFriend(request.Id, request.FriendId), _friendRepository.DeleteFriend(request.Id, request.FriendId));
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
                
        }

        public async Task AddPlatformFriend(string platformAccount, int type, FriendInfo friendInfo)
        {
            //查询该玩家是否是注册玩家
           
            var response = await _mqGetIdCleint.GetResponseExt<GetIdByPlatformMqCommand, BodyResponse<GetIdByPlatformMqResponse>>
                 (new GetIdByPlatformMqCommand(platformAccount, type));
            if (response.Message.StatusCode != StatusCodeDefines.Success)
            {
                return;
            }
            long friendId = response.Message.Body.Id;
            if (friendInfo._friends == null || !friendInfo._friends.TryGetValue(friendId, out var info))
            {
                using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(friendId, "FriendInfo")))
                {
                    await AddFriend(friendInfo.Id, friendId, FriendTypes.PlatformFriend);
                    return;
                }
                    
            }
            if (info.Type != FriendTypes.PlatformFriend)
            {
                using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(friendId, "FriendInfo")))
                {
                    await _redis.DeleteFriend(friendInfo.Id, friendId);
                    await Task.WhenAll(_redis.AddFriend(friendInfo.Id, friendId, FriendTypes.PlatformFriend),
                        _friendRepository.UpdateFriend(friendInfo.Id, friendId, FriendTypes.PlatformFriend));
                }
                    
            }
            

        }

        public async Task<BodyResponse<NullBody>> Handle(UploadPlatformFriendsCommand request, CancellationToken cancellationToken)
        {
            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            {
                await locker.LockAsync();

                var info = await LoadFriends(request.Id);
                List<Task> tasks = new List<Task>();
                foreach (var one in request.PlatformFriends)
                {
                    tasks.Add(AddPlatformFriend(one.PlatformAccount, one.Type, info));
                }
                await Task.WhenAll(tasks);
                return new BodyResponse<NullBody>(StatusCodeDefines.Success);
            }
        }

        public async Task<BodyResponse<GetFriendInfoMqResponse>> Handle(GetFriendInfoCommand request, CancellationToken cancellationToken)
        {

            using (var locker = _redis.Locker(KeyGenHelper.GenUserKey(request.Id, "FriendInfo")))
            {
                await LoadToRedis(request.Id);
                var info = await _redis.GetOneFriendInfo(request.Id, request.OtherId);
                if (info != null)
                {
                    return new BodyResponse<GetFriendInfoMqResponse>(StatusCodeDefines.Success, null,
                        new GetFriendInfoMqResponse(info.Type));
                }
                if (await _redis.IsAlreadyApplyed(request.Id, request.OtherId))
                {
                    return new BodyResponse<GetFriendInfoMqResponse>(StatusCodeDefines.Success, null,
                        new GetFriendInfoMqResponse(FriendTypes.Applyed));
                }
                else
                {
                    return new BodyResponse<GetFriendInfoMqResponse>(StatusCodeDefines.Success, null,
                        new GetFriendInfoMqResponse(FriendTypes.None));
                }
            }







        }
    }
}
