using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reward.Application.Services;
using Reward.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Reward.Domain.Models;
using Commons.Extenssions.Defines;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RewardController : ControllerBase
    {

        /// <summary>
        /// 查询注册奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RegisterRewardVM> QueryRegisterReward([FromHeader]long id)
        {
            return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 领取注册奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetRegisterReward([FromHeader]long id)
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 查询登录奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<LoginRewardVM> QueryLoginReward([FromHeader]long id)
        {
            return new BodyResponse<LoginRewardVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 获取登录奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetLoginReward([FromHeader]long id)
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 查询破产奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<BankruptcyInfoVM> QueryBankruptcy([FromHeader]long id)
        {
            return new BodyResponse<BankruptcyInfoVM>();
        }

        /// <summary>
        /// 领取破产奖励
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetBankruptcy([FromHeader]long id)
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }
    }
}