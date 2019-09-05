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

namespace UploadFile.WebApi.Controllers
{


    /// <summary>
    /// 账号相关操作
    /// </summary>
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IConfiguration _config;
        public UploadFileController(IConfiguration config)
        {
            _config = config;
        }


        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param files="model"></param>
        /// <returns>按顺序返回服务器生成的名字</returns>
        [HttpPost]
        public async Task<BodyResponse<List<string>>> UploadImages(List<IFormFile> files)
        {
            var fileName = Path.GetRandomFileName();
            List<string> listFiles = new List<string>();
            foreach (var formFile in files)
            {
                listFiles.Add(await StoreFile(formFile, _config["FilePath"], "UploadImage", fileName));
            };
            return new BodyResponse<List<string>>(StatusCodeDefines.Success, null, listFiles);
        }

        public static async Task<string> StoreFile(IFormFile formFile, string basePath, string path, string fileName)
        {
            string fullPath = Path.Combine(basePath, path);
            if (false == System.IO.Directory.Exists(fullPath))
            {
                System.IO.Directory.CreateDirectory(fullPath);
            }
            string fullName = Path.Combine(fullPath, fileName + Path.GetExtension(formFile.FileName));
            
            if (formFile.Length > 0 && formFile.Length < 1024 * 1024 * 100)
            {
                using (var stream = new FileStream(fullName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                    return Path.Combine(path, fileName + Path.GetExtension(formFile.FileName));
                }
            }
            return null;
        }

        [HttpPost]
        public  BodyResponse<List<string>> TestPost(string file1, string file2)
        {
           
            return new BodyResponse<List<string>>(StatusCodeDefines.Success, null, new List<string>() { file1, file2});
    
        }
    }
}