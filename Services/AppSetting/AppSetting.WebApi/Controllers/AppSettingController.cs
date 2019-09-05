using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppSetting.Application.Services;
using AppSetting.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using AppSetting.Domain.Models;
using Commons.Extenssions.Defines;

namespace AppSetting.WebApi.Controllers
{
    /// <summary>
    /// 设置和反馈相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class AppSettingController : ControllerBase
    {
        private readonly IAppSettingService _service;

        public AppSettingController(IAppSettingService service)
        {
            _service = service;
        }


        [HttpPost]
        //[Route("AppSettingLogin")]
        public  BodyResponse<string> Fadeback([FromBody] UserFadeback faceBack)
        {
           
             return new BodyResponse<string>(StatusCodeDefines.Success, null, null);
    
        }
    }
}