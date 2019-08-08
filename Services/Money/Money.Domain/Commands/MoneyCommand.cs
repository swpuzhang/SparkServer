using Commons.Domain.Commands;
using Commons.Infrastruct;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Domain.Models;
using Money.Domain.Models;
using Commons.MqCommands;

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

    public class BuyInCommand : Command<BodyResponse<MoneyMqResponse>>
    {
        public BuyInCommand(long id, long minBuy, long maxBuy)
        {
            Id = id;
            MinBuy = minBuy;
            MaxBuy = maxBuy;
        }

        public long Id { get; private set; }
        public long MinBuy { get; private set; }
        public long MaxBuy { get; private set; }

    }

    public class AddMoneyCommand : Command<BodyResponse<MoneyMqResponse>>
    {
        public AddMoneyCommand(long id, long addCoins, long addCarry)
        {
            Id = id;
            AddCoins = addCoins;
            AddCarry = addCarry;
        }

        public long Id { get; private set; }
        public long AddCoins { get; private set; }
        public long AddCarry { get; private set; }

    }
}
