using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friend.Domain.Models
{
    public class OneFriendInfo
    {
        public OneFriendInfo(long friendId, FriendTypes type)
        {
            FriendId = friendId;
            Type = type;
        }
        /// <summary>
        /// 好友ID
        /// </summary>
        public long FriendId { get; set; }
        
        public FriendTypes Type { get; set; }
    }
    public class FriendInfo : UserEntity
    {

        

        public FriendInfo()
        {

        }

        public FriendInfo(long id, Dictionary<long, OneFriendInfo> friends)
        {
            Id = id;
            _friends = friends;
        }

        public Dictionary<long, OneFriendInfo> _friends { get; set; }
        
    }

    public class OneFriendVM
    {
        public OneFriendVM()
        {
        }

        public OneFriendVM(long id, string platformAccount,
            string userName, int sex, string headUrl, int type, FriendTypes friendType)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
            FriendType = friendType;
        }

        public long Id { get; set; }
        public string PlatformAccount { get; set; }

        public string UserName { get; set; }

        public int Sex { get; set; }

        public string HeadUrl { get; set; }

        /// <summary>
        /// 平台类型
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// 好友类型 0 游戏好友 1 平台好友, 非好友忽略
        /// </summary>
        public FriendTypes FriendType { get; set; }
    }
    /// <summary>
    /// 好友信息
    /// </summary>
    public class FriendVM
    {
        public List<OneFriendVM> Friends { get; set; }
    }

    /// <summary>
    /// 平台账号信息
    /// </summary>
    public class PlatformFriendVM
    {
        /// <summary>
        /// 平台账号
        /// </summary>
        public string PlatformAccount { get; set; }
        /// <summary>
        /// 平台类型
        /// </summary>
        public int Type { get; set; }
    }
}
