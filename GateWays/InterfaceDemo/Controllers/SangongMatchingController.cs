using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Extenssions;
using Sangong.Application.ViewModels;
using Sangong.Domain.Models;

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 匹配相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class SangongMatchingController : ControllerBase
    {
        /// <summary>
        /// Playnow 接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<MatchingResponseVM> PlayNow()
        {
            return new BodyResponse<MatchingResponseVM>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }


        /// <summary>
        /// 获取房间列表接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<GetBlindRoomListResponse> GetBlindRoomList()
        {
            return new BodyResponse<GetBlindRoomListResponse>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }

        /// <summary>
        /// 底注匹配接口
        /// </summary>
        /// <param name="blind"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<MatchingResponseVM> BlindMatching( long blind)
        {

            return new BodyResponse<MatchingResponseVM>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }
    }
}