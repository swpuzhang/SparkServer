using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Money.Application.Services;
using Money.Application.ViewModels;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Money.Domain.Models;
using Commons.Extenssions.Defines;

namespace Money.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class MoneyController : ControllerBase
    {
        [HttpGet]
        public string Test()
        {
            return "Test";
        }
    }
}