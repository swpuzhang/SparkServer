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

namespace Reward.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RewardController : ControllerBase
    {
        private readonly IRewardService _service;

        public RewardController(IRewardService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<BodyResponse<RegisterRewardVM>> QueryRegisterReward([FromHeader]long id)
        {
            return await _service.QueryRegisterReward(id);
        }

        [HttpGet]
        public async Task<BodyResponse<RewardInfoVM>> GetRegisterReward([FromHeader]long id)
        {
            return await _service.GetRegisterReward(id);
        }
        public async Task<BodyResponse<LoginRewardVM>> QueryLoginReward([FromHeader]long id)
        {
            return await _service.QueryLoginReward(id);
        }

        public async Task<BodyResponse<RewardInfoVM>> GetLoginReward([FromHeader]long id)
        {
            return await _service.GetLoginReward(id);
        }
        public async Task<BodyResponse<BankruptcyInfoVM>> QueryBankruptcy([FromHeader]long id)
        {
            return await _service.QueryBankruptcy(id);
        }
    }
}