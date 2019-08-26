using Account.Domain.Models;
using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class AccountDetailVM
    {
        public string PlatformAccount { get; set; }

        public string UserName { get; set; }

        public int Sex { get; set; }

        public string HeadUrl { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType Type { get; set; }

        public LevelInfoVM LevelInfo { get; set; }

        public GameInfoVM GameInfo { get; set; }

        public MoneyInfo MoneyInfo { get;  set; }

        public AccountDetailVM()
        {

        }

        [JsonConstructor]
        public AccountDetailVM(string platformAccount, string userName,
            int sex, string headUrl, AccountType type,
            LevelInfoVM levelInfo, GameInfoVM gameInfo, MoneyInfo moneyInfo = null)
        {
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
            LevelInfo = levelInfo;
            GameInfo = gameInfo;
            MoneyInfo = moneyInfo;
        }
    }
    public class OtherAccountDetaiVM
    {
        public string PlatformAccount { get; set; }

        public string UserName { get; set; }

        public int Sex { get; set; }

        public string HeadUrl { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType Type { get; set; }

        public LevelInfoVM LevelInfo { get; set; }

        public GameInfoVM GameInfo { get; set; }

        public MoneyInfo MoneyInfo { get; set; }

        /// <summary>
        /// 好友类型 -1 非好友 0 游戏好友 1 平台好友
        /// </summary>
        public FriendTypes FriendType { get; set; }

        public OtherAccountDetaiVM()
        {

        }

        [JsonConstructor]
        public OtherAccountDetaiVM(string platformAccount, string userName,
            int sex, string headUrl, AccountType type,
            LevelInfoVM levelInfo, GameInfoVM gameInfo,
            MoneyInfo moneyInfo, FriendTypes friendType)
        {
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
            LevelInfo = levelInfo;
            GameInfo = gameInfo;
            MoneyInfo = moneyInfo;
            FriendType = friendType;
        }
    }
}
