using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
                 
    public class AccountInfo : UserEntity
    {
        
        public string PlatformAccount { get; private set; }
  
        public string UserName { get; private set; }
        
        public int Sex { get; private set; }
    
        public string HeadUrl { get; private set; }

        public AccountInfo()
        {

        }

        public AccountInfo(int id, string account, string name, int sex, string head)
        {
            Id = id;
            PlatformAccount = account;
            UserName = name;
            Sex = sex;
            HeadUrl = head;
        }
    }
}
