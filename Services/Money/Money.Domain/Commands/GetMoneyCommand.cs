using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Money.Domain.Models;

namespace Money.Domain.Commands
{
    //public class LoginCommands : Command<BodyResponse<MoneyResponse>
    public class GetMoneyCommand : Command<BodyResponse<MoneyInfo>>
    {
        public long Id { get; private set; }
        public GetMoneyCommand(long id)
        {
            Id = id;
        }
    }
     
}
