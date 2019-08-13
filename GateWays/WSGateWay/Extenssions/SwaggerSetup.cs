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

namespace WSGateWay.Extenssions
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
                if (Environment.OSVersion.Platform == PlatformID.MacOSX ||
                        Environment.OSVersion.Platform == PlatformID.Unix)
                {

                    string home = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    basePath = Path.Combine(home, "work/SwaggerInterface");
                }
                else
                {
                    string curPath = ApplicationEnvironment.ApplicationBasePath;
                    int index = curPath.LastIndexOf("Spark");
                    basePath = curPath.Substring(0, index + 5) + "/work/SwaggerInterface";
                }

                var files = Directory.GetFiles(basePath, "*.xml");
                foreach (var oneFile in files)
                {
                    var xmlPath = Path.Combine(basePath, oneFile);
                    c.IncludeXmlComments(xmlPath, true);
                }

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
