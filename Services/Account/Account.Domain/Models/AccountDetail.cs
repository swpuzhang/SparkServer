using Commons.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
    public class AccountDetail : UserEntity
    {

        public string PlatformAccount { get; private set; }

        public string UserName { get; private set; }

        public int Sex { get; private set; }

        public string HeadUrl { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int Type { get; private set; }

        public LevelInfo  LevelInfo { get; private set;}

        public GameInfo GameInfo { get; private set; }

        public MoneyInfo MoneyInfo { get; private set; }

        public AccountDetail()
        {

        }

        [JsonConstructor]
        public AccountDetail(long id, string platformAccount, string userName,
            int sex, string headUrl, int type,
            LevelInfo levelInfo, GameInfo gameInfo, MoneyInfo moneyInfo)
        {
            Id = id;
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
    
}
