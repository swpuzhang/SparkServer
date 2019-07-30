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

        public static StatuCodeDefines CheckToken(string str, long id, long expireSec = 3600 * 24)
        {
            
            try
            {
                string decStr = Secret.DecryptAES(str);
                UserToken token = JsonConvert.DeserializeObject<UserToken>(decStr);
                if (token == null || token.Id != id)
                {
                    return StatuCodeDefines.TokenError;
                }
                long tnow = DateTimeHelper.ToTimeStamp(DateTime.Now);
                if (tnow - token.GenTime >= expireSec)
                {
                    return StatuCodeDefines.TokenExpiredPleaseRelogin;
                }
                return StatuCodeDefines.Success;
            }
            catch(Exception)
            {
                return StatuCodeDefines.TokenError;
            }
            
        }

        public static StatuCodeDefines ParseToken(string str, out long id, long expireSec = 3600 * 24)
        {
            id = 0;
            try
            {
                string decStr = Secret.DecryptAES(str);
                UserToken token = JsonConvert.DeserializeObject<UserToken>(decStr);
                if (token == null)
                {
                    return StatuCodeDefines.TokenError;
                }
                long tnow = DateTimeHelper.ToTimeStamp(DateTime.Now);
                if (tnow - token.GenTime >= expireSec)
                {
                    return StatuCodeDefines.TokenExpiredPleaseRelogin;
                }
                id = token.Id;
                return StatuCodeDefines.Success;
            }
            catch (Exception)
            {
                return StatuCodeDefines.TokenError;
            }

        }

    }
}
