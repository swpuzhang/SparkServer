using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WSGateWay.Services
{
    public class CommonService
    {
        public KeyValuePair<bool, long> TokenValidation(string token)
        {
            KeyValuePair<bool, long> pair= new KeyValuePair<bool, long>(false, 0);

            return pair;
        }
    }
}
