using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MsgCenter.Application.Services;
using MsgCenter.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using MsgCenter.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.MqCommands;

namespace MsgCenter.WebApi.Controllers
{
    /// <summary>
    /// 消息中心相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MsgCenterController : ControllerBase
    {
        private readonly IMsgCenterService _service;

        public MsgCenterController(IMsgCenterService service)
        {
            _service = service;
        }


        /// <summary>
        /// 获取系统通知消息
        /// </summary>
        [HttpGet]
        public  Task<BodyResponse<UserMsgs>> GetUserSysMsgs([FromHeader] long id)
        {

            return _service.GetUserMsgs(id, MsgTypes.Sys);
    
        }
        /// <summary>
        /// 获取奖励消息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<UserMsgs>> GetUserRewardMsgs([FromHeader] long id)
        {

            return _service.GetUserMsgs(id, MsgTypes.Reward);

        }

        /// <summary>
        /// 已经读取消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<NullBody>> ReadedMsg([FromHeader] long id, MsgTypes msgType, string msgId)
        {
            return _service.ReadedMsg(id, msgType, msgId);
        }

        /// <summary>
        /// 获取消息奖励
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<NullBody>> RecieveMsgReward([FromHeader] long id, string msgId)
        {
            return _service.RecieveMsgReward(id, msgId);
        }

        /// <summary>
        /// 删除消息
        /// </summary>
        /// <param name="id"></param>
        /// <param name="msgType"></param>
        /// <param name="msgId"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<NullBody>> DeleteMsg([FromHeader] long id, MsgTypes msgType, string msgId)
        {
            return _service.DeleteMsg(id, msgType, msgId);
        }

       /// <summary>
       /// 一键已读
       /// </summary>
       /// <param name="id"></param>
       /// <param name="msgType"></param>
       /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<NullBody>> ReadedAllMsg([FromHeader] long id, MsgTypes msgType)
        {

            return _service.ReadedAllMsg(id, msgType);

        }
        /// <summary>
        /// 一键领取奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<List<RewardInfo>>> RecieveAllMsgReward([FromHeader] long id)
        {

            return _service.RecieveAllMsgReward(id);

        }
    }
}