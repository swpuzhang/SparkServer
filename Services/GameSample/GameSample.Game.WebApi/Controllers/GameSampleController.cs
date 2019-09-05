using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameSample.Application.Services;
using GameSample.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using GameSample.Domain.Models;
using Commons.Extenssions.Defines;

namespace GameSample.Game.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class GameSampleController : ControllerBase
    {
        private readonly IGameSampleAppService _service;

        public GameSampleController(IGameSampleAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[Route("GameSampleLogin")]
        public BodyResponse<GameSampleInfo> Login([FromBody] GameSampleVM model)
        {

            return new BodyResponse<GameSampleInfo>(StatusCodeDefines.FieldError, null, null);

        }

    }
       
    
}