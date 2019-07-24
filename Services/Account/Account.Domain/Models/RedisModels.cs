using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
    public class LoginCheckInfo
    {
        public LoginCheckInfo()
        {
        }

        public LoginCheckInfo(long id, string platformAccount, int type)
        {
            Id = id;
            PlatformAccount = platformAccount;
            Type = type;
        }

        public Int64 Id { get; private set; }

        public string PlatformAccount { get; private set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; private set; }
    }
}
