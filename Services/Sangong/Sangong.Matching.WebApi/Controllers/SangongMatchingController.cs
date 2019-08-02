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

namespace Sangong.Matching.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SangongMatchingController : ControllerBase
    {
        private readonly ISangongAppService _service;

        public SangongMatchingController(ISangongAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// Playnow 接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<SangongMatchingResponseVM> PlayNow([FromHeader]long id)
        {
            return new BodyResponse<SangongMatchingResponseVM>(StatuCodeDefines.FieldError, null, null);    
        }

        [HttpGet("{id}")]
        //[Route("SangongSelf/id")]
        public BodyResponse<SangongVM> GetSelfSangong([FromHeader]long id)
        {
            return new BodyResponse<SangongVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public BodyResponse<SangongVM> GetOtherSangong(Int64 otherId)
        {
            var sangong = _service.GetById(otherId);
            return new BodyResponse<SangongVM>(0, null, sangong);
        }
    }
}