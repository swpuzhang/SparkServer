using System;
using System.Collections.Generic;
using System.Text;

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

    public class GetMoneyMqResponse
    {
        public GetMoneyMqResponse(long curChips, long curDiamonds, long maxChips, long maxDiamonds)
        {
            CurChips = curChips;
            CurDiamonds = curDiamonds;
            MaxChips = maxChips;
            MaxDiamonds = maxDiamonds;
        }

        public long CurChips { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxChips { get; private set; }
        public long MaxDiamonds { get; private set; }
    }
}
