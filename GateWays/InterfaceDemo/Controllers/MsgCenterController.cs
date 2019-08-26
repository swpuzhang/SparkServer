using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using MsgCenter.Domain.Models;
using Commons.MqCommands;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 消息中心相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MsgCenterController : ControllerBase
    {
      

        /// <summary>
        /// 获取系统通知消息
        /// </summary>
        [HttpGet]
        public  BodyResponse<UserMsgs> GetUserSysMsgs()
        {

            return new BodyResponse<UserMsgs>();
    
        }
        /// <summary>
        /// 获取奖励消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<UserMsgs> GetUserRewardMsgs()
        {

            return new BodyResponse<UserMsgs>();

        }

        /// <summary>
        /// 已经读取消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> ReadedMsg(MsgTypes msgType, string msgId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 获取消息奖励
        /// </summary>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> RecieveMsgReward(string msgId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> DeleteMsg(MsgTypes msgType, string msgId)
        {
            return new BodyResponse<NullBody>();
        }

       /// <summary>
       /// 一键已读
       /// </summary>
       /// <param name="msgType"></param>
       /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> ReadedAllMsg(MsgTypes msgType)
        {

            return new BodyResponse<NullBody>();

        }
        /// <summary>
        /// 一键领取奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<List<RewardInfo>> RecieveAllMsgReward()
        {

            return new BodyResponse<List<RewardInfo>>();

        }
    }
}