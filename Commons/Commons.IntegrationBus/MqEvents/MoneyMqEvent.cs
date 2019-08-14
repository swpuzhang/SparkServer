using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Message.MqEvents
{
    /// <summary>
    /// 筹码变更通知
    /// </summary>
    public class MoneyChangeMqEvent
    {
        public long Id { get; private set; }
        public long CurCoins { get; private set; }
        public long CurDiamonds { get; private set; }
        public long MaxCoins { get; private set; }
        public long MaxDiamonds { get; private set; }
        
    }
}
