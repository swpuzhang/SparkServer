using AutoMapper;
using Commons.Extenssions.Defines;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Commons.Domain.Models
{
    public  class NullBody
    {

    }

    public class BodyResponse<T> where T :class
    {
        public BodyResponse()
        {

        }
        [JsonConstructor]
        public BodyResponse(StatuCodeDefines statusCode, List<string> errorInfos, T body = null)
        {
            StatusCode = statusCode;
            ErrorInfos = errorInfos;
            Body = body;
        }

        public BodyResponse<V>  MapResponse<V>(IMapper mapper) where V : class
        {
            return new BodyResponse<V>(StatusCode, ErrorInfos, mapper.Map<V>(Body));
        }

        public StatuCodeDefines StatusCode { get; private set; }
        public List<string> ErrorInfos { get; private set; }
        public T Body { get; set; }
    }
}
