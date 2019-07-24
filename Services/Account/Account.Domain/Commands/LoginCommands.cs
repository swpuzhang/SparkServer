using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Account.Domain.Models;

namespace Account.Domain.Commands
{
    //public class LoginCommands : Command<HasBodyResponse<AccountResponse>
    public class LoginCommand : Command<HasBodyResponse<AccountResponse>>
    {
        public AccountInfo Info { get; private set; }
        public LoginCommand(AccountInfo info)
        {
            Info = info;
        }
    }
     
}
