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

        public TokenCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, string [] filterApi)
        {
            if (filterApi != null)
            {
                string findPath = filterApi.First(x => x == context.Request.Path);
                if (!string.IsNullOrEmpty(findPath))
                {
                    await _next(context);
                    return;
                }
            }
            
            var token = context.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(token))
            {
                 await context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new MiddleResponse(StatuCodeDefines.TokenError, null)));
                return;
                
            }
            long id = 0;
            var status = TokenHelper.ParseToken(token, out id);
            if (status != StatuCodeDefines.Success)
            {
                await context.Response.WriteAsync(JsonConvert.SerializeObject(
                    new MiddleResponse(status, null)));
                return;
            }
            context.Request.Headers["id"] = id.ToString();
            await _next(context);
        }

       
    }

    public static class TokenCheckMiddlewareExtenssion
    {
        public static IApplicationBuilder UseTokenCheck(
           this IApplicationBuilder builder, params string [] filterApi)
        {
            return builder.UseMiddleware<TokenCheckMiddleware>();
        }
    }

}
