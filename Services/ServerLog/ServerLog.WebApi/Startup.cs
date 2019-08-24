using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServerLog.WebApi.Extenssions;
using Commons.Extenssions;

namespace ServerLog.WebApi
{
    public class Startup
    {
        public static readonly string Namespace = typeof(Startup).Namespace;
        public static readonly string ServiceName = Namespace.Substring(0, Namespace.IndexOf("."));
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMongoServices(Configuration);
            services.AddAutoMapperSetup();
            services.RegisterSwaggerServices();
            services.AddServices(Configuration);
            ContainerBuilder builder = new ContainerBuilder();
            services.AddMassTransitService(Configuration, builder);
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigSwaggerServices();
            app.ConfigServices(Configuration);
            app.UseTokenCheck("/api/Account/Login");
            app.UseMvc();
        }
    }
}
