using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using MsgCenter.Domain.Models;
using Commons.MqCommands;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MsgCenterController : ControllerBase
    {
      

        /// <summary>
        /// 获取系统通知消息
        /// </summary>
        [HttpGet]
        public  BodyResponse<UserMsgs> GetUserSysMsgs([FromHeader] long id)
        {

            return new BodyResponse<UserMsgs>();
    
        }
        /// <summary>
        /// 获取奖励消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<UserMsgs> GetUserRewardMsgs([FromHeader] long id)
        {

            return new BodyResponse<UserMsgs>();

        }

        /// <summary>
        /// 已经读取消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> ReadedMsg([FromHeader] long id, MsgTypes msgType, string msgId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 获取消息奖励
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> RecieveMsgReward([FromHeader] long id, string msgId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> DeleteMsg([FromHeader] long id, MsgTypes msgType, string msgId)
        {
            return new BodyResponse<NullBody>();
        }

       /// <summary>
       /// 一键已读
       /// </summary>
       /// <param name="id"></param>
       /// <param name="msgType"></param>
       /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> ReadedAllMsg([FromHeader] long id, MsgTypes msgType)
        {

            return new BodyResponse<NullBody>();

        }
        /// <summary>
        /// 一键领取奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<List<RewardInfo>> RecieveAllMsgReward([FromHeader] long id)
        {

            return new BodyResponse<List<RewardInfo>>();

        }
    }
}