using Commons.Domain.Models;
using Newtonsoft.Json;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reward.Domain.Models
{
    /// <summary>
    ///单个房间打牌活动
    /// </summary>
    public class OneRoomActivityInfoVM
    {
        /// <summary>
        /// 子任务ID
        /// </summary>
        public string  SubId { get; set; }
        /// <summary>
        /// 当前计数
        /// </summary>
        public long CurCount { get; set; }
        /// <summary>
        /// 需要的总计数
        /// </summary>
        public long NeedCount { get; set; }
        /// <summary>
        /// 是否完成该任务
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 奖励金币数
        /// </summary>
        
        public long RewardCoins { get; set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get; set; }
    }

    /// <summary>
    /// 一个打牌活动
    /// </summary>
    public class OneGameActivityInfoVM
    {
        public OneGameActivityInfoVM(string activityId, string title, List<OneRoomActivityInfoVM> roomInfos)
        {
            ActivityId = activityId;
            Title = title;
            RoomInfos = roomInfos;
        }

        /// <summary>
        /// 唯一活动ID
        /// </summary>
        public string ActivityId { get;  set; }
        /// <summary>
        /// 活动标题
        /// </summary>
        public string Title { get;  set; }
        /// <summary>
        /// 所有场次打牌详情
        /// </summary>
        public List<OneRoomActivityInfoVM> RoomInfos { get;  set; }
    }

    /// <summary>
    /// 所有活动信息
    /// </summary>
    public class ActivityInfoVM
    {

        /// <summary>
        ///所有打牌活动
        /// </summary>
        public List<OneGameActivityInfoVM> AllGameActivitys { get; set; }
        /// <summary>
        ///所有赢牌活动
        /// </summary>
        public List<OneGameActivityInfoVM> AllWinActivitys { get; set; }
    }

    public class GameSubActInfo : UserEntity
    {
        [JsonConstructor]
        public GameSubActInfo(long curCount, int state)
        {
            CurCount = curCount;
            State = state;
        }

        public long CurCount { get; set; }
        public int State { get; set; }
    }

    public class OneGameActivityInfo : UserEntity
    {
        public OneGameActivityInfo(string activityId, Dictionary<string, GameSubActInfo> countProgress)
        {
            ActivityId = activityId;
            CountProgress = countProgress;
        }

        public string ActivityId { get; private set; }
        public Dictionary<string, GameSubActInfo> CountProgress { get; private set; }
    }
}
