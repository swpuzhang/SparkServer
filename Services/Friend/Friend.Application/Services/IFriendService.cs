using Friend.Application.ViewModels;
using Friend.Domain.Models;
using Commons.Domain.Models;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Friend.Application.Services
{
    public interface IFriendService
    {
        Task<BodyResponse<NullBody>> ApplyAddFriend(long id, long friendId);
        Task<BodyResponse<NullBody>> AgreeAddFriend(long id, long friendId);
        Task<BodyResponse<FriendVM>> GetFriends(long id);
        Task<BodyResponse<FriendVM>> GetApplys(long id);
        Task<BodyResponse<NullBody>> IgnoreApply(long id, long friendId);
        Task<BodyResponse<NullBody>> DeleteFriend(long id, long friendId);
        Task<BodyResponse<NullBody>> UploadPlatformFriends(long id, List<PlatformFriendVM> platformFriends);
    }
}
