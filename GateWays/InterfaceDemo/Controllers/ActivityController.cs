using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Reward.Application.Services;
using Reward.Domain.Models;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 活动相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ActivityController : Controller
    {
       

        /// <summary>
        /// 查询活动
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<ActivityInfoVM> QueryActivity()
        {
            return new BodyResponse<ActivityInfoVM>(Commons.Extenssions.Defines.StatusCodeDefines.Success);
        }

       /// <summary>
       /// 获取打牌任务奖励
       /// </summary>
       /// <param name="activityId"></param>
       /// <param name="subId"></param>
       /// <returns></returns>
        [HttpGet]
        public BodyResponse<RewardInfoVM> GetGameActReward( string activityId, string subId)
        {
            return new BodyResponse<RewardInfoVM>(Commons.Extenssions.Defines.StatusCodeDefines.Success);
        }
    }
}