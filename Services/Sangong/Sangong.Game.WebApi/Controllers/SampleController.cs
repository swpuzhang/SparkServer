using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sample.Application.Services;
using Sample.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Sample.Domain.Models;
using Commons.Extenssions.Defines;

namespace Sangong.Game.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SampleController : ControllerBase
    {
        private readonly ISampleAppService _service;

        public SampleController(ISampleAppService service)
        {
            _service = service;
        }


        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("SampleLogin")]
        public async Task<HasBodyResponse<SampleResponse>> Login([FromBody] SampleVM model)
        {
            if (!ModelState.IsValid)
            {
                return new HasBodyResponse<SampleResponse>(StatuCodeDefines.FieldError, null, null);
                
            }
            return await _service.Login(model);
        }

        [HttpGet("{id}")]
        //[Route("SampleSelf/id")]
        public HasBodyResponse<SampleVM> GetSelfSample(Int64 id)
        {
            return new HasBodyResponse<SampleVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public HasBodyResponse<SampleVM> GetOtherSample(Int64 otherId)
        {
            var sample = _service.GetById(otherId);
            return new HasBodyResponse<SampleVM>(0, null, sample);
        }
    }
}