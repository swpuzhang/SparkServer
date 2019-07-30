using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace MatchingSangong.Api.Extenssions
{
    

    public static class SwaggerSetup
    {
        public static void RegisterSwaggerService(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                c.DescribeAllEnumsAsStrings();
                
                var basePath = Directory.GetCurrentDirectory();
                var xmlPath = Path.Combine(basePath, Assembly.GetExecutingAssembly().GetName().Name + ".xml");
                var viewModelXmlPath = Path.Combine(basePath, $"{Startup.ServiceName}.Application.xml");
                c.IncludeXmlComments(xmlPath);
                c.IncludeXmlComments(viewModelXmlPath);
                
            });
        }

        public static void ConfigSwaggerService(this IApplicationBuilder app)
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
