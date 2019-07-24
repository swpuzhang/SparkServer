using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Extenssions
{
    public static class KeyGenHelper
    {
        /// <summary>
        /// 生成跟uid相关的key  userid|key1|key2
        /// </summary>
        /// <param name="key1"></param>
        /// <param name="ps"></param>
        /// <returns></returns>
        public static string GenKey(string key1, params string[] ps)
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.Append(key1);
            
            foreach (var key in ps)
            {
                strbuilder.Append("-");
                strbuilder.Append(key);
            }
            return strbuilder.ToString();
        }

        public static string GenUserKey(Int64 userid, params string[] ps)
        {
            StringBuilder strbuilder = new StringBuilder();
            strbuilder.Append(userid.ToString());

            foreach (var key in ps)
            {
                strbuilder.Append("|");
                strbuilder.Append(key);
            }
            return strbuilder.ToString();
        }
    }
}
