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

namespace Sample.WebApi.Controllers
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
        public  BodyResponse<SampleInfo> Login([FromBody] SampleVM model)
        {
           
             return new BodyResponse<SampleInfo>(StatuCodeDefines.FieldError, null, null);
    
        }

        [HttpGet("{id}")]
        //[Route("SampleSelf/id")]
        public BodyResponse<SampleVM> GetSelfSample([FromHeader]long id)
        {
            return new BodyResponse<SampleVM>(0, null, _service.GetById(id));
            
        }

        [HttpGet]
        public BodyResponse<SampleVM> GetOtherSample(Int64 otherId)
        {
            var sample = _service.GetById(otherId);
            return new BodyResponse<SampleVM>(0, null, sample);
        }
    }
}