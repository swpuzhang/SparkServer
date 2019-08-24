using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Message.MqEvents
{
    /// <summary>
    /// 筹码变更通知
    /// </summary>
    public class MoneyChangedMqEvent : MQBaseMessage
    {
        public MoneyChangedMqEvent(long coinsChangeCount, long diamondsChangeCount)
        {
            CoinsChangeCount = coinsChangeCount;
            DiamondsChangeCount = diamondsChangeCount;
        }
        [JsonConstructor]
        public MoneyChangedMqEvent(long id, long curCoins, long curDiamonds, 
            long maxCoins, long maxDiamonds, 
            long coinsChangeCount, long diamondsChangeCount)
        {
            Id = id;
            CurCoins = curCoins;
            CurDiamonds = curDiamonds;
            MaxCoins = maxCoins;
            MaxDiamonds = maxDiamonds;
            CoinsChangeCount = coinsChangeCount;
            DiamondsChangeCount = diamondsChangeCount;
        }

        public long Id { get; set; }
        public long CurCoins { get; set; }
        public long CurDiamonds { get; set; }
        public long MaxCoins { get; set; }
        public long MaxDiamonds { get; set; }
        /// <summary>
        /// 变化的金币数量
        /// </summary>
        public long CoinsChangeCount { get; set; }
        /// <summary>
        /// 变化的砖石数量
        /// </summary>
        public long DiamondsChangeCount { get; set; }
        public AddReason reason { get; set; }
        
    }
}
