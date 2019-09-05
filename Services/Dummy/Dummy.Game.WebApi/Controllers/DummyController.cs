using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dummy.Application.Services;
using Dummy.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Dummy.Domain.Models;
using Commons.Extenssions.Defines;

namespace Dummy.Game.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly IDummyAppService _service;

        public DummyController(IDummyAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("DummyLogin")]
        public BodyResponse<DummyInfo> Login([FromBody] DummyVM model)
        {

            return new BodyResponse<DummyInfo>(StatusCodeDefines.FieldError, null, null);

        }

    }
       
    
}