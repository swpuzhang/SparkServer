using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Application.Services;
using Account.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Account.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Extenssions;

namespace Account.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作api/Account/Login
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountAppService _service;

        public AccountController(IAccountAppService service)
        {
            _service = service;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BodyResponse<AccountResponseVM>> Login([FromBody] AccountVM model)
        {
            return await _service.Login(model);
        }

        /// <summary>
        /// 获取自己的账号信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<BodyResponse<AccountDetailVM>> GetSelfAccount([FromHeader]long id)
        {
            var response = await _service.GetSelfAccount(id);
            return response;

        }

        /// <summary>
        /// 获取其他玩家的信息
        /// </summary>
        /// <param name="otherId">其他玩家ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BodyResponse<AccountDetailVM>> GetOtherAccount(Int64 otherId)
        {

            var response = await _service.GetSelfAccount(otherId);
            return response;
        }

    }
}