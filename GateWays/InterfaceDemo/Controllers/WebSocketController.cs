using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Account.Application.Services;
using Account.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Account.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Extenssions;
using WSGateWay.ViewModels;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// websocket 相关接口只能看， 不能调用， 不是HTTP请求
    /// </summary>
    [Route("[Action]")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {

        /// <summary>
        /// 建立Websocket连接后， 第一个必须调用的接口
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public BodyResponse<NullBody> LoginRequest(LoginRequest request)
        {
            return new BodyResponse<NullBody>(StatusCodeDefines.Error, null, null);
        }

    }
}