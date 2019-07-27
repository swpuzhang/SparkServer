using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Domain.Models
{
    public class LoginCheckInfo
    {
        public LoginCheckInfo()
        {
        }

        public LoginCheckInfo(long id, string platformMoney, int type)
        {
            Id = id;
            PlatformMoney = platformMoney;
            Type = type;
        }

        public Int64 Id { get; private set; }

        public string PlatformMoney { get; private set; }
        
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; private set; }
    }
}
