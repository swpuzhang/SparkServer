using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Money.Domain.Models
{
                 
    public class MoneyInfo : UserEntity
    {

        public string PlatformMoney { get; private set; }
  
        public string UserName { get; private set; }
        
        public int Sex { get; private set; }
    
        public string HeadUrl { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int Type { get; set; }

        public MoneyInfo()
        {

        }

        public MoneyInfo(long id, string money, string name, int sex, string head, int type)
        {
            Id = id;
            PlatformMoney = money;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
            Type = type;
        }


        public override bool Equals(object obj)
        {
            return obj is MoneyInfo info &&
                   base.Equals(obj) &&
                   PlatformMoney == info.PlatformMoney &&
                   UserName == info.UserName &&
                   Sex == info.Sex &&
                   HeadUrl == info.HeadUrl;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
