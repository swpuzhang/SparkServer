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

namespace Sangong.Game.WebApi.Controllers
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
        public BodyResponse<SangongInfo> Login([FromBody] SangongVM model)
        {

            return new BodyResponse<SangongInfo>(StatusCodeDefines.FieldError, null, null);

        }

    }
       
    
}