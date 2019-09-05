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
using Commons.Extenssions;

namespace GameSample.Matching.WebApi.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class GameSampleMatchingController : ControllerBase
    {
        private readonly IGameSampleMatchingService _service;

        public GameSampleMatchingController(IGameSampleMatchingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Playnow 接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<BodyResponse<MatchingResponseVM>> PlayNow([FromHeader]long id)
        {
            var response = await OneThreadSynchronizationContext.UserRequest(id, _service.Playnow);
            return response;
        }

        [HttpGet]
        public async Task<BodyResponse<GetBlindRoomListResponse>> GetBlindRoomList([FromHeader]long id)
        {
            var response = await OneThreadSynchronizationContext
                .UserRequest<long, BodyResponse< GetBlindRoomListResponse >> 
                    (id, _service.GetBlindRoomList);
            return response;
        }

        [HttpGet]
        public async Task<BodyResponse<MatchingResponseVM>> BlindMatching([FromHeader]long id, long blind)
        {
            
            return await OneThreadSynchronizationContext.UserRequest(id, blind, _service.BlindMatching);
        }
    }
}