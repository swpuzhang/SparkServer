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
using Commons.Extenssions;

namespace Sangong.Matching.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SangongMatchingController : ControllerBase
    {
        private readonly ISangongMatchingService _service;

        public SangongMatchingController(ISangongMatchingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Playnow 接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<BodyResponse<SangongMatchingResponseVM>> PlayNow([FromHeader]long id)
        {
            return await OneThreadSynchronizationContext.UserRequest(id, _service.Playnow);
        }
    }
}