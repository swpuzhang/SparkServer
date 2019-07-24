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

namespace Account.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
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
        //[Route("AccountLogin")]
        public async Task<HasBodyResponse<AccountResponse>> Login([FromBody] AccountVM model)
        {
            if (!ModelState.IsValid)
            {
                return new HasBodyResponse<AccountResponse>(StatuCodeDefines.FieldError, null, null);
                
            }
            return await _service.Login(model);
        }

        [HttpGet("{id}")]
        //[Route("AccountSelf/id")]
        public HasBodyResponse<AccountVM> GetSelfAccount(Int64 id)
        {
            return new HasBodyResponse<AccountVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public HasBodyResponse<AccountVM> GetOtherAccount(Int64 otherId)
        {
            var account = _service.GetById(otherId);
            return new HasBodyResponse<AccountVM>(0, null, account);
        }
    }
}