using AutoMapper;
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

    public class BodyResponse<T> : BaseResponse
    {
        public BodyResponse()
        {

        }
        public BodyResponse(StatuCodeDefines statusCode, List<string> errorInfos, T body) :
            base(statusCode, errorInfos)
        {
            Body = body;
        }

        public BodyResponse<u>  MapResponse<u>(IMapper mapper)
        {
            return new BodyResponse<u>(StatusCode, ErrorInfos, mapper.Map<u>(Body));
        }


        public T Body { get; set; }
    }
}
