using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Friend.Application.ViewModels;
using Friend.Domain;
using Friend.Domain.Commands;
using Friend.Domain.Models;
using Friend.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Friend.Application.Services
{
    public class FriendService : IFriendService
    {
        private readonly IFriendInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public FriendService(IFriendInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }

        public Task<BodyResponse<NullBody>> ApplyAddFriend(long id, long friendId)
        {
            return _bus.SendCommand(new ApplyAddFriendCommand(id, friendId));
        }

        public Task<BodyResponse<NullBody>> AgreeAddFriend(long id, long friendId)
        {
            return _bus.SendCommand(new AgreeAddFriendCommand(id, friendId));
        }

        public Task<BodyResponse<FriendVM>> GetFriends(long id)
        {
            return _bus.SendCommand(new GetFriendsCommand(id));
        }

        public Task<BodyResponse<FriendVM>> GetApplys(long id)
        {
            return _bus.SendCommand(new GetApplysCommand(id));
        }

        public Task<BodyResponse<NullBody>> IgnoreApply(long id, long friendId)
        {
            return _bus.SendCommand(new IgnoreApplyCommand(id, friendId));
        }
        public Task<BodyResponse<NullBody>> DeleteFriend(long id, long friendId)
        {
            return _bus.SendCommand(new DeleteFriendCommand(id, friendId));
        }

        public Task<BodyResponse<NullBody>> UploadPlatformFriends(long id, List<PlatformFriendVM> platformFriends)
        {
            return _bus.SendCommand(new UploadPlatformFriendsCommand(id, platformFriends));
        }
    }
}
