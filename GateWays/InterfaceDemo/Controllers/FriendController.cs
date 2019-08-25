using System;
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

namespace InterfaceDemo.Controllers
{
    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
       

        /// <summary>
        /// 申请加好友
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<NullBody> ApplyAddFriend([FromBody] long id, long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 同意添加好友
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> AgreeAddFriend([FromBody] long id, long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        [HttpGet]
        public BodyResponse<FriendVM> GetFriends([FromBody] long id)
        {
            return new BodyResponse<FriendVM>();
        }

        [HttpGet]
        public BodyResponse<FriendVM> GetApplys([FromBody] long id)
        {
            return new BodyResponse<FriendVM>();
        }

        [HttpGet]
        public BodyResponse<NullBody> IgnoreApply([FromBody] long id, long friendId)
        {
            return new BodyResponse<NullBody>();
        }
        [HttpGet]
        public BodyResponse<NullBody> DeleteFriend([FromBody] long id, long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        [HttpPost]
        public BodyResponse<NullBody> UploadPlatformFriends([FromBody] long id, [FromBody] List<PlatformFriendVM> platformFriends)
        {
            return new BodyResponse<NullBody>();
        }
    }
}