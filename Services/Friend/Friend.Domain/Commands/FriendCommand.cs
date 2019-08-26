using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Friend.Domain.Models;
using Commons.MqCommands;

namespace Friend.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<FriendResponse>
    public class ApplyAddFriendCommand : Command<BodyResponse<NullBody>>
    {
        public long Id { get; private set; }
        public long FriendId { get; private set; }

        public ApplyAddFriendCommand(long id, long friendId)
        {
            Id = id;
            FriendId = friendId;
        }
    }

    public class AgreeAddFriendCommand : Command<BodyResponse<NullBody>>
    {
        public long Id { get; private set; }
        public long FriendId { get; private set; }

        public AgreeAddFriendCommand(long id, long friendId)
        {
            Id = id;
            FriendId = friendId;
        }
    }

    public class GetFriendsCommand : Command<BodyResponse<FriendVM>>
    {
        public long Id { get; private set; }

        public GetFriendsCommand(long id)
        {
            Id = id;
        }
    }

    public class GetApplysCommand : Command<BodyResponse<FriendVM>>
    {
        public long Id { get; private set; }

        public GetApplysCommand(long id)
        {
            Id = id;
        }
    }

    public class IgnoreApplyCommand : Command<BodyResponse<NullBody>>
    {
        public long Id { get; private set; }
        public long FriendId { get; private set; }

        public IgnoreApplyCommand(long id, long friendId)
        {
            Id = id;
            FriendId = friendId;
        }
    }

    public class DeleteFriendCommand : Command<BodyResponse<NullBody>>
    {
        public long Id { get; private set; }
        public long FriendId { get; private set; }

        public DeleteFriendCommand(long id, long friendId)
        {
            Id = id;
            FriendId = friendId;
        }
    }

    public class UploadPlatformFriendsCommand : Command<BodyResponse<NullBody>>
    {
        public long Id { get; private set; }
        public List<PlatformFriendVM> PlatformFriends { get; private set; }

        public UploadPlatformFriendsCommand(long id, List<PlatformFriendVM> platformFriends)
        {
            Id = id;
            PlatformFriends = platformFriends;
        }
    }

    public class GetFriendInfoCommand : Command<BodyResponse<GetFriendInfoMqResponse>>
    {
        public GetFriendInfoCommand(long id, long otherId)
        {
            Id = id;
            OtherId = otherId;
        }

        public long Id { get; private set; }
        public long OtherId { get; private set; }
    }
    
}
