using Account.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.ViewModels
{
    public class AccountDetailVM
    {
        public string PlatformAccount { get; private set; }

        public string UserName { get; private set; }

        public int Sex { get; private set; }

        public string HeadUrl { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int Type { get; private set; }

        public LevelInfo LevelInfo { get; private set; }

        public GameInfo GameInfo { get; private set; }

        public AccountDetailVM()
        {

        }

        [JsonConstructor]
        public AccountDetailVM(string account, string name,
            int sex, string head, int type,
            LevelInfo levelInfo, GameInfo gameInfo)
        {
            PlatformAccount = account;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
            Type = type;
            LevelInfo = levelInfo;
            GameInfo = gameInfo;
        }
    }
}
