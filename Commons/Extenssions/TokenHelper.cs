using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Commons.Extenssions
{
    public class UserToken
    {
        public long Id { get; set; }
        public long GenTime { get; set; }
    }

    public static class TokenHelper
    {
        public static string GenToken(long uid)
        {
            UserToken token = new UserToken
            {
                Id = uid,
                GenTime = DateTimeHelper.ToTimeStamp(DateTime.Now)
            };
            string inputstr = JsonConvert.SerializeObject(token);
            
            return Secret.EncryptAES(inputstr);
        }

        public static StatusCodeDefines CheckToken(string str, long id, long expireSec = 3600 * 24)
        {
            
            try
            {
                string decStr = Secret.DecryptAES(str);
                UserToken token = JsonConvert.DeserializeObject<UserToken>(decStr);
                if (token == null || token.Id != id)
                {
                    return StatusCodeDefines.TokenError;
                }
                long tnow = DateTimeHelper.ToTimeStamp(DateTime.Now);
                if (tnow - token.GenTime >= expireSec)
                {
                    return StatusCodeDefines.TokenExpiredPleaseRelogin;
                }
                return StatusCodeDefines.Success;
            }
            catch(Exception)
            {
                return StatusCodeDefines.TokenError;
            }
            
        }

        public static StatusCodeDefines ParseToken(string str, out long id, long expireSec = 3600 * 24)
        {
            id = 0;
            try
            {
                string decStr = Secret.DecryptAES(str);
                UserToken token = JsonConvert.DeserializeObject<UserToken>(decStr);
                if (token == null)
                {
                    return StatusCodeDefines.TokenError;
                }
                long tnow = DateTimeHelper.ToTimeStamp(DateTime.Now);
                if (tnow - token.GenTime >= expireSec)
                {
                    return StatusCodeDefines.TokenExpiredPleaseRelogin;
                }
                id = token.Id;
                return StatusCodeDefines.Success;
            }
            catch (Exception)
            {
                return StatusCodeDefines.TokenError;
            }

        }

    }
}
