﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Friend.Application.Services;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Friend.Domain.Models;
using Commons.Extenssions.Defines;

namespace Friend.WebApi.Controllers
{
    /// <summary>
    /// 好友相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
        private readonly IFriendService _service;

        public FriendController(IFriendService service)
        {
            _service = service;
        }

        /// <summary>
        /// 申请加好友
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public  Task<BodyResponse<NullBody>> ApplyAddFriend([FromHeader] long id, long friendId)
        {
            return _service.ApplyAddFriend(id, friendId);
        }

        /// <summary>
        /// 同意添加好友
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<BodyResponse<NullBody>> AgreeAddFriend([FromHeader] long id, long friendId)
        {
            return _service.AgreeAddFriend(id, friendId);
        }

        [HttpGet]
        public Task<BodyResponse<FriendVM>> GetFriends([FromHeader] long id)
        {
            return _service.GetFriends(id);
        }

        [HttpGet]
        public Task<BodyResponse<FriendVM>> GetApplys([FromHeader] long id)
        {
            return _service.GetApplys(id);
        }

        [HttpGet]
        public Task<BodyResponse<NullBody>> IgnoreApply([FromHeader] long id, long friendId)
        {
            return _service.IgnoreApply(id, friendId);
        }
        [HttpGet]
        public Task<BodyResponse<NullBody>> DeleteFriend([FromHeader] long id, long friendId)
        {
            return _service.DeleteFriend(id, friendId);
        }

        [HttpPost]
        public Task<BodyResponse<NullBody>> UploadPlatformFriends([FromHeader] long id, [FromBody] List<PlatformFriendVM> platformFriends)
        {
            return _service.UploadPlatformFriends(id, platformFriends);
        }
    }
}