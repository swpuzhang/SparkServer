using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;
using Account.Domain.Models;
using Newtonsoft.Json;

namespace Account.Application.ViewModels
{
    /// <summary>
    /// 登录账号信息
    /// </summary>
    public class AccountVM
    {
        /// <summary>
        /// 平台账号
        /// </summary>
        public string PlatformAccount { get;  set; }

        /// <summary>
        /// 平台账号名
        /// </summary>
        public string UserName { get;  set; }

        /// <summary>
        /// 性别0 男， 1女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 平台头像url
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType Type { get; set; }

        public AccountVM()
        {

        }

        [JsonConstructor]
        public AccountVM(string platformAccount, string userName, int sex, string headUrl, AccountType type)
        {
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
        }
    }

    public class AccountResponseVM
    {
        private AccountResponseVM()
        {

        }

        public AccountResponseVM(string platformAccount,
            string userName, int sex, string headUrl, AccountType type,
            string token, long curCoins,
            long curDiamonds, string longConnectHost, long id, bool isRegister)
        {
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            Type = type;
            LongConnectHost = longConnectHost;
            Id = id;
            IsRegister = isRegister;
        }
        /// <summary>
        ///玩家唯一Id 
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 平台账号
        /// </summary>
        public string PlatformAccount { get; set; }

        /// <summary>
        /// 平台账号名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 性别0 男， 1女
        /// </summary>
        public int Sex { get; set; }

        /// <summary>
        /// 平台头像url
        /// </summary>
        public string HeadUrl { get; set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public AccountType Type { get; set; }

        /// <summary>
        /// 当前金币
        /// </summary>
        public long CurCoins { get; set; }

        /// <summary>
        /// 当前砖石数
        /// </summary>
        public long CurDiamonds { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 长连接地址
        /// </summary>
        public string LongConnectHost { get; set; }
        /// <summary>
        /// 是否是注册，还是普通登录
        /// </summary>
        public bool IsRegister { get; set; }
    }

}
