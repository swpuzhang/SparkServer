using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{
    public class BaseResponse
    {
        public BaseResponse()
        {
        }

        [JsonConstructor]
        public BaseResponse(StatuCodeDefines statusCode, List<string> errorInfos)
        {
            StatusCode = statusCode;
            ErrorInfos = errorInfos;
        }

        public StatuCodeDefines StatusCode { get; private set; }
        public List<string> ErrorInfos { get; private set; }
    }

    public class HasBodyResponse<T> : BaseResponse
    {
        public HasBodyResponse()
        {

        }
        public HasBodyResponse(StatuCodeDefines statusCode, List<string> errorInfos, T body) :
            base(statusCode, errorInfos)
        {
            Body = body;
        }
        public T Body { get; set; }
    }
}
