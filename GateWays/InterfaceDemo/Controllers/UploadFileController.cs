using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commons.Infrastruct;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace InterfaceDemo.Controllers
{


    /// <summary>
    /// 上传文件和推片
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
      

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param files="model"></param>
        /// <returns>按顺序返回服务器生成的图片名字</returns>
        [HttpPost]
        public  BodyResponse<List<string>> UploadImages(List<IFormFile> files)
        {
          
            return new BodyResponse<List<string>>(StatusCodeDefines.Success, null, null);
        }

    }
}