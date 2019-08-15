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

namespace ApiGateWay.Controllers
{
    /// <summary>
    /// 匹配相关接口 只能看不能调用
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
        public BodyResponse<MatchingResponseVM> PlayNow([FromHeader]long id)
        {
            return new BodyResponse<MatchingResponseVM>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }

        /// <summary>
        /// 获取房间列表接口
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public  BodyResponse<GetBlindRoomListResponse> GetBlindRoomList([FromHeader]long id)
        {
            return new BodyResponse<GetBlindRoomListResponse>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }

        /// <summary>
        /// 底注匹配接口
        /// </summary>
        /// <param name="id"></param>
        /// <param name="blind"></param>
        /// <returns></returns>
        public BodyResponse<MatchingResponseVM> BlindMatching([FromHeader]long id, long blind)
        {

            return new BodyResponse<MatchingResponseVM>(StatusCodeDefines.Error, new List<string>() { "just demo" });
        }
    }
}