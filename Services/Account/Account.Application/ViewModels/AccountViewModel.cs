using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Commons.Extenssions.Defines;
using Account.Domain.Models;

namespace Account.Application.ViewModels
{
    /// <summary>
    /// 接口字段
    /// </summary>
    public class AccountVM
    {
        public string PlatformAccount { get; private set; }

        public string UserName { get; private set; }

        public int Sex { get; private set; }

        public string HeadUrl { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int Type { get; set; }

        public AccountVM()
        {

        }

        public AccountVM(string account, string name, int sex, string head, int type)
        {
            PlatformAccount = account;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
            Type = type;
        }
    }

    public class AccountResponseVM
    {
        private AccountResponseVM()
        {

        }

        public AccountResponseVM( string platformAccount,
            string userName, int sex, string headUrl,
            string token, long curChips,
            long curDiamonds = 0,
            long maxChips = 0,
            long maxDiamonds = 0)
        {
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            CurChips = curChips;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public string PlatformAccount { get; private set; }
        public string UserName { get; private set; }
        public int Sex { get; private set; }
        public string HeadUrl { get; private set; }

        public long CurChips { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxChips { get; private set; }
        public long MaxDiamonds { get; private set; }

        public string Token { get; set; }
    }

}
