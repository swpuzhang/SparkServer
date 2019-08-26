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
    /// 奖励相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RewardController : ControllerBase
    {

        /// <summary>
        /// 查询注册奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RegisterRewardVM> QueryRegisterReward()
        {
            return new BodyResponse<RegisterRewardVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 领取注册奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetRegisterReward()
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 查询登录奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<LoginRewardVM> QueryLoginReward()
        {
            return new BodyResponse<LoginRewardVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 获取登录奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetLoginReward()
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }

        /// <summary>
        /// 查询破产奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<BankruptcyInfoVM> QueryBankruptcy()
        {
            return new BodyResponse<BankruptcyInfoVM>();
        }

        /// <summary>
        /// 领取破产奖励
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<RewardInfoVM> GetBankruptcy()
        {
            return new BodyResponse<RewardInfoVM>(StatusCodeDefines.Success);
        }
    }
}