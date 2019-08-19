using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Reward.Application.Services;
using Reward.Domain.Models;

namespace Reward.WebApi.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ActivityController : Controller
    {
        private readonly IActivityService _service;

        public ActivityController(IActivityService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<BodyResponse<ActivityInfoVM>> QueryActivity([FromHeader]long id)
        {
            return await _service.QueryActivity(id);
        }

        [HttpGet]
        public async Task<BodyResponse<RewardInfoVM>> GetRegisterReward([FromHeader]long id, string activityId, string subId)
        {
            return await _service.GetGameActReward(id, activityId, subId);
        }
    }
}