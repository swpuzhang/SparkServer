using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sangong.Application.Services;
using Sangong.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Sangong.Domain.Models;
using Commons.Extenssions.Defines;

namespace Sangong.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SangongController : ControllerBase
    {
        private readonly ISangongAppService _service;

        public SangongController(ISangongAppService service)
        {
            _service = service;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("SangongLogin")]
        public async Task<HasBodyResponse<SangongResponse>> Login([FromBody] SangongVM model)
        {
            if (!ModelState.IsValid)
            {
                return new HasBodyResponse<SangongResponse>(StatuCodeDefines.FieldError, null, null);
                
            }
            return await _service.Login(model);
        }

        [HttpGet("{id}")]
        //[Route("SangongSelf/id")]
        public HasBodyResponse<SangongVM> GetSelfSangong(Int64 id)
        {
            return new HasBodyResponse<SangongVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public HasBodyResponse<SangongVM> GetOtherSangong(Int64 otherId)
        {
            var sangong = _service.GetById(otherId);
            return new HasBodyResponse<SangongVM>(0, null, sangong);
        }
    }
}