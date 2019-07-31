using Commons.Extenssions.Defines;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Commons.Extenssions
{
    public class MiddleResponse
    {
        public MiddleResponse()
        {
        }

        [JsonConstructor]
        public MiddleResponse(StatuCodeDefines statusCode, List<string> errorInfos)
        {
            StatusCode = statusCode;
            ErrorInfos = errorInfos;
        }

        public StatuCodeDefines StatusCode { get; private set; }
        public List<string> ErrorInfos { get; private set; }
    }

    public class TokenCheckMiddleware
    {
        private readonly RequestDelegate _next;
        List<string> _filterApis;

        public TokenCheckMiddleware(RequestDelegate next, List<string> filterApis)
        {
            _next = next;
            _filterApis = filterApis;
        }

        public Task InvokeAsync(HttpContext context)
        {
            //可选参数始终不为null
            
           
           
            string findPath = _filterApis.FirstOrDefault(x => x == context.Request.Path);
            if (!string.IsNullOrEmpty(findPath))
            {
                return _next(context);
           }

            var token = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new MiddleResponse(StatuCodeDefines.TokenError, null)));
                ;
                
            }
            long id = 0;
            var status = TokenHelper.ParseToken(token, out id);
            if (status != StatuCodeDefines.Success)
            {
                return context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new MiddleResponse(status, null)));
            }
            context.Request.Headers["id"] = id.ToString();
            return _next(context);
        }

       
    }

    public static class TokenCheckMiddlewareExtenssion
    {
        public static IApplicationBuilder UseTokenCheck(
           this IApplicationBuilder builder, params string [] filterApi)
        {
            return builder.UseMiddleware<TokenCheckMiddleware>(filterApi.ToList());
        }
    }

}
