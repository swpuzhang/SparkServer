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
    /// 好友相关接口
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class FriendController : ControllerBase
    {
       

        /// <summary>
        /// 申请加好友
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public  BodyResponse<NullBody> ApplyAddFriend(long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        /// <summary>
        /// 同意添加好友
        /// 
        /// </summary>
        /// <param name="friendId"></param>
        /// <returns></returns>
        [HttpGet]
        public BodyResponse<NullBody> AgreeAddFriend(long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        [HttpGet]
        public BodyResponse<FriendVM> GetFriends()
        {
            return new BodyResponse<FriendVM>();
        }

        [HttpGet]
        public BodyResponse<FriendVM> GetApplys()
        {
            return new BodyResponse<FriendVM>();
        }

        [HttpGet]
        public BodyResponse<NullBody> IgnoreApply(long friendId)
        {
            return new BodyResponse<NullBody>();
        }
        [HttpGet]
        public BodyResponse<NullBody> DeleteFriend(long friendId)
        {
            return new BodyResponse<NullBody>();
        }

        [HttpPost]
        public BodyResponse<NullBody> UploadPlatformFriends([FromBody] List<PlatformFriendVM> platformFriends)
        {
            return new BodyResponse<NullBody>();
        }
    }
}