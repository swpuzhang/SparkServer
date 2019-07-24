using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Domain.Models
{
    public class AccountResponse
    {

        private AccountResponse()
        {

        }

        public AccountResponse(long id, string platformAccount, string userName, int sex, string headUrl, string token)
        {
            Id = id;
            PlatformAccount = platformAccount;
            UserName = userName;
            Sex = sex;
            HeadUrl = headUrl;
            Token = token;
        }

        public Int64 Id { get; set; }
        public string PlatformAccount { get; set; }
        public string UserName { get; set; }
        public int Sex { get; set; }
        public string HeadUrl { get; set; }

        public string Token { get; set; }
    }

}
