using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Money.Application.Services;
using Money.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Money.Domain.Models;
using Commons.Extenssions.Defines;

namespace Money.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        private readonly IMoneyAppService _service;

        public MoneyController(IMoneyAppService service)
        {
            _service = service;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("MoneyLogin")]
        public async Task<HasBodyResponse<MoneyResponse>> Login([FromBody] MoneyVM model)
        {
            if (!ModelState.IsValid)
            {
                return new HasBodyResponse<MoneyResponse>(StatuCodeDefines.FieldError, null, null);
                
            }
            return await _service.Login(model);
        }

        [HttpGet("{id}")]
        //[Route("MoneySelf/id")]
        public HasBodyResponse<MoneyVM> GetSelfMoney(Int64 id)
        {
            return new HasBodyResponse<MoneyVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public HasBodyResponse<MoneyVM> GetOtherMoney(Int64 otherId)
        {
            var money = _service.GetById(otherId);
            return new HasBodyResponse<MoneyVM>(0, null, money);
        }
    }
}