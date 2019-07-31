using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commons.Extenssions
{
    public class HttpHeaderFilter : IOperationFilter
    {
        public void Apply(Operation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null) operation.Parameters = new List<IParameter>();

            var attrs = context.ApiDescription.ActionDescriptor.AttributeRouteInfo;
            var descriptor = context.ApiDescription.ActionDescriptor as ControllerActionDescriptor;
            if (descriptor != null)
            {
                var actionAttributes = descriptor.MethodInfo.GetCustomAttributes(inherit: true);
                bool isAnonymous = actionAttributes.Any(a => a is AllowAnonymousAttribute);
                //非匿名的方法,链接中添加accesstoken值
                if (!isAnonymous)
                {
                    operation.Parameters.Add(new NonBodyParameter()
                    {
                        Name = "Authorization",
                        In = "header",//query header body path formData
                        Type = "string",
                        Required = false //是否必选
                    });
                }
            }
        }
        
    }
}
