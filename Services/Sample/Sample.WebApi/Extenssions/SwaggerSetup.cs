using Commons.Extenssions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.DotNet.PlatformAbstractions;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sample.WebApi.Extenssions
{
    

    public static class SwaggerSetup
    {
        public static void RegisterSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.OperationFilter<HttpHeaderFilter>();
                c.DescribeAllEnumsAsStrings();

                string basePath;
                var env = services.BuildServiceProvider().GetService<IHostingEnvironment>();
                if (env.IsDevelopment())
                {
                    basePath = Path.Combine(Directory.GetCurrentDirectory(), "../../../SwaggerInterface");
                }
                else
                {
                    basePath = "~/work/SwaggerInterface";
                }
                var xmlPath = Path.Combine(basePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                var viewModelXmlPath = Path.Combine(basePath, $"{Startup.ServiceName}.Application.xml");
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(viewModelXmlPath);
                
            });
        }

        public static void ConfigSwaggerServices(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });

        }
    }
}
