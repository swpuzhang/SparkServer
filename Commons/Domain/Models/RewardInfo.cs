using Commons.Extenssions.Defines;
using System;
using System.Collections.Generic;
using System.Text;
using MassTransit;
using Commons.MqCommands;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Commons.Domain.Models
{
    public class RewardInfo
    {
        [JsonConstructor]
        protected RewardInfo(RewardTypes rewardType, int goodsId, 
            long count, string name, string description, 
            string imageUrl, AddReason reason)
        {
            RewardType = rewardType;
            GoodsId = goodsId;
            Count = count;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            Reason = reason;
        }

        public IBaseRewardMethod GenRewardMethod(RewardTypes rewardType)
        {
            switch (rewardType)
            {
                case RewardTypes.Coins:
                    {
                        return new CoinsRewardMethod();
                    }
            }
            return null;
        }

        public RewardTypes RewardType { get; set; }
        /// <summary>
        /// 物品ID. 对于一些道具该字段有意义
        /// </summary>
        public int GoodsId { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public long Count { get; set; }
        /// <summary>
        /// 奖励名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 图片
        /// </summary>
        public string ImageUrl { get; set; }
        public AddReason Reason { get; set; }
    }

    public interface IBaseRewardMethod
    {
        Task AddReward(IBusControl bus, long id, RewardInfo info);
    }

    public class CoinsRewardMethod : IBaseRewardMethod
    {

        public Task AddReward(IBusControl bus, long id, RewardInfo info)
        {
            return bus.Publish(new AddMoneyMqCommand(id, info.Count, 0, info.Reason));
        }

      
    }
}
