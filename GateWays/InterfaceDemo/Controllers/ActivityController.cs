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
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ActivityController : Controller
    {
       

        /// <summary>
        /// 查询活动
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<ActivityInfoVM> QueryActivity([FromHeader]long id)
        {
            return new BodyResponse<ActivityInfoVM>(Commons.Extenssions.Defines.StatusCodeDefines.Success);
        }

       /// <summary>
       /// 获取打牌任务奖励
       /// </summary>
       /// <param name="id"></param>
       /// <param name="activityId"></param>
       /// <param name="subId"></param>
       /// <returns></returns>
        [HttpGet]
        public BodyResponse<RewardInfoVM> GetGameActReward([FromHeader]long id, string activityId, string subId)
        {
            return new BodyResponse<RewardInfoVM>(Commons.Extenssions.Defines.StatusCodeDefines.Success);
        }
    }
}