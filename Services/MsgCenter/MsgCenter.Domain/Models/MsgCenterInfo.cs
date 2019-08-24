using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgCenter.Domain.Models
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
        public MsgTypes MsgType { get; set; }
        public string MsgId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public MsgStates MsgState { get; set; }
        public long MsgTime { get; set; }   
        public List<RewardInfo> RewardInfo { get; set; }
    }


    public class UserMsgs
    {
        public UserMsgs(List<UserMsgInfo> msgs)
        {
            Msgs = msgs;
        }

        public List<UserMsgInfo> Msgs { get; set; }
    }

}
