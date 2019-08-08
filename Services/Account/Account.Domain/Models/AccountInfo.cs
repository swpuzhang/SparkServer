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

        public int Type { get; set; }

        public AccountInfo()
        {

        }

        [JsonConstructor]
        public AccountInfo(long id, string platformAccount, string userName, int sex, string headUrl, int type)
        {
           
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
            
        }


        public override bool Equals(object obj)
        {
            return obj is AccountInfo info &&
                   PlatformAccount == info.PlatformAccount &&
                   UserName == info.UserName &&
                   Sex == info.Sex &&
                   HeadUrl == info.HeadUrl &&
                   Type == info.Type;
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
            string token, MoneyInfo moneyInfo, 
            string longConnectHost)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            MoneyInfo = moneyInfo;
            LongConnectHost = longConnectHost;
        }

        public Int64 Id { get; private set; }
        public string PlatformAccount { get; private set; }
        public string UserName { get; private set; }
        public int Sex { get; set; }
        public string HeadUrl { get; private set; }

        public MoneyInfo MoneyInfo { get; private set; }
        

        public string Token { get; private set; }

        public string LongConnectHost { get; set; }
    }

}
