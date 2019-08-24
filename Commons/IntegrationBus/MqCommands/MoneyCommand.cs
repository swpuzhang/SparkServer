using System;
using System.Collections.Generic;
using System.Text;
using Commons.Extenssions.Defines;

namespace Commons.MqCommands
{
    public class GetMoneyMqCommand
    {
        public GetMoneyMqCommand(long id)
        {
            Id = id;
        }

        public long Id { get; private set; }
    }

    public class MoneyBaseCommand
    {

        public AddReason Reason;

        public MoneyBaseCommand()
        {
        }

        public MoneyBaseCommand(AddReason reason)
        {
            Reason = reason;
        }
    }


    public class BuyInMqCommand : MoneyBaseCommand
    {
        public BuyInMqCommand(long id, long minBuy, long maxBuy, AddReason reason)
            :base(reason)
        {
            Id = id;
            MinBuy = minBuy;
            MaxBuy = maxBuy;
        }

        public long Id { get; private set; }
        public long MinBuy { get; private set; }
        public long MaxBuy { get; private set; }
    }

    public class MoneyMqResponse 
    {
        public MoneyMqResponse(long id, long curCoins, long curDiamonds, 
            long maxCoins, long maxDiamonds, long carry)
        {
            Id = id;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxCoins = maxCoins;
            MaxDiamonds = maxDiamonds;
            Carry = carry;
        }

        public long Id { get; private set; }
        public long CurCoins { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxCoins { get; private set; }
        public long MaxDiamonds { get; private set; }
        public long Carry { get; private set; }
    }

    public class AddMoneyMqCommand : MoneyBaseCommand
    {
        public AddMoneyMqCommand(long id, long addCoins, long addCarry, AddReason reason)
            :base(reason)
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
