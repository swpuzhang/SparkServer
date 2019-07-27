using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Money.Domain.Models;

namespace Money.Domain.Commands
{
    //public class LoginCommands : Command<HasBodyResponse<MoneyResponse>
    public class LoginCommand : Command<HasBodyResponse<MoneyResponse>>
    {
        public MoneyInfo Info { get; private set; }
        public LoginCommand(MoneyInfo info)
        {
            Info = info;
        }
    }
     
}
