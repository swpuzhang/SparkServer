using Commons.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.MqCommands
{
    public enum MsgStates
    {
        Unread = 0,
        Readed = 1,
        /// <summary>
        /// 奖励是否已经领取
        /// </summary>
        Recieved = 2
    }

    public enum MsgTypes
    {
        Sys,
        Reward,
    }

    public class UserMsgInfo
    {
        [JsonConstructor]
        public UserMsgInfo(MsgTypes msgType, string msgId, string title,
            string content, MsgStates msgState, long msgTime, List<RewardInfo> rewardInfo)
        {
            MsgType = msgType;
            MsgId = msgId;
            Title = title;
            Content = content;
            MsgState = msgState;
            MsgTime = msgTime;
            RewardInfo = rewardInfo;
        }

        public MsgTypes MsgType { get; set; }
        public string MsgId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public MsgStates MsgState { get; set; }
        public long MsgTime { get; set; }
        public List<RewardInfo> RewardInfo { get; set; }
    }
    public class PushUserMsgMqCommand
    {
        public PushUserMsgMqCommand(long id, UserMsgInfo msg)
        {
            Id = id;
            Msg = msg;
        }

        public long Id { get; set; }
        public UserMsgInfo Msg { get; set; }

    }
}
