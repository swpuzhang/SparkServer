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
using Commons.Message.MqEvents;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// websocket server向APP推送消息
    /// </summary>
    [Route("[Action]")]
    [ApiController]
    public class WSNotifyController : ControllerBase
    {

        /// <summary>
        /// 金钱变更通知
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public ToServerResponse<NullBody> ToAppRequestMoneyChangedMqEvent(ToAppRequest<MoneyChangedMqEvent> request)
        {
            return new ToServerResponse<NullBody>();
        }

    }
}