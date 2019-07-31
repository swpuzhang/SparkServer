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
    public class GetMoneyCommand : Command<HasBodyResponse<MoneyInfo>>
    {
        public long Id { get; private set; }
        public GetMoneyCommand(long id)
        {
            Id = id;
        }
    }
     
}
