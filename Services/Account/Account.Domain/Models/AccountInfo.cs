using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Account.Domain.Models
{
               
    public class AccountInfo : UserEntity
    {
        
        public string PlatformAccount { get; private set; }
  
        public string UserName { get; private set; }
        
        public int Sex { get; private set; }
    
        public string HeadUrl { get; private set; }

        /// <summary>
        /// 账号类型
        /// </summary>
        public int Type { get; set; }

        public AccountInfo()
        {

        }

        [JsonConstructor]
        public AccountInfo(long id, string account, string name, int sex, string head, int type)
        {
            Id = id;
            PlatformAccount = account;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
            Type = type;
        }


        public override bool Equals(object obj)
        {
            return obj is AccountInfo info &&
                   base.Equals(obj) &&
                   PlatformAccount == info.PlatformAccount &&
                   UserName == info.UserName &&
                   Sex == info.Sex &&
                   HeadUrl == info.HeadUrl;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class AccountResponse
    {

        private AccountResponse()
        {

        }

        public AccountResponse(long id, string platformAccount,
            string userName, int sex, string headUrl,
            string token
, MoneyInfo moneyInfo)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            MoneyInfo = moneyInfo;
        }

        public Int64 Id { get; private set; }
        public string PlatformAccount { get; private set; }
        public string UserName { get; private set; }
        public int Sex { get; set; }
        public string HeadUrl { get; private set; }

        public MoneyInfo MoneyInfo { get; private set; }
        

        public string Token { get; private set; }
    }

}
