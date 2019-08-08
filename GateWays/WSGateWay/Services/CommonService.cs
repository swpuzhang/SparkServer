using Commons.Extenssions;
using Commons.Extenssions.Defines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSGateWay.Services
{
    public class CommonService : ICommonService
    {
        public KeyValuePair<bool, long> TokenValidation(string token)
        {

            long id = 0;
            var status = TokenHelper.ParseToken(token, out id);
            if (status != StatuCodeDefines.Success)
            {
                return new KeyValuePair<bool, long>(false, id);
            }

            return new KeyValuePair<bool, long>(true, id); ;
        }
    }
}
