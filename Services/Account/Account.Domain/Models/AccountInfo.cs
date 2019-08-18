using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Commons.MqCommands;

namespace Account.Domain.Models
{
               
    public class AccountInfo : UserEntity
    {
        public static string className = "AccountInfo";
        public string PlatformAccount { get; private set; }
  
        public string UserName { get; private set; }
        
        public int Sex { get; private set; }
    
        public string HeadUrl { get; private set; }

        public int Type { get; set; }

        public DateTime RegisterDate { get; private set; }

        public GetAccountBaseInfoMqResponse.SomeFlags Flags { get; set; }

        public AccountInfo()
        {

        }

        [JsonConstructor]
        public AccountInfo(long id, string platformAccount, string userName,
            int sex, string headUrl, int type, DateTime registerDate, 
            GetAccountBaseInfoMqResponse.SomeFlags flags)
        {

            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Type = type;
            RegisterDate = registerDate;
            Flags = flags;
        }


        public override bool Equals(object obj)
        {
            return obj is AccountInfo info &&
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
            string token, MoneyInfo moneyInfo,
            string longConnectHost, bool isRegister, int type)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
            MoneyInfo = moneyInfo;
            LongConnectHost = longConnectHost;
            IsRegister = isRegister;
            Type = type;
        }

        public Int64 Id { get; private set; }
        public string PlatformAccount { get; private set; }
        public string UserName { get; private set; }
        public int Sex { get; set; }
        public string HeadUrl { get; private set; }
        public int Type { get; private set; }

        public MoneyInfo MoneyInfo { get; private set; }
        

        public string Token { get; private set; }

        public string LongConnectHost { get; set; }

        public bool IsRegister { get; set; }
    }

}
