using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WSGateWay.Hubs;
using WSGateWay.Extenssions;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace WSGateWay
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
        public static IServiceProvider Provider { get; private set; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMongoService(Configuration);
            services.RegisterSwaggerService();
            services.RegisterServices();
            services.AddAutoMapperSetup();
            services.AddSingleton<RedisHelper>(new RedisHelper(Configuration["redis:ConnectionString"]));
            ContainerBuilder builder = new ContainerBuilder();
            services.AddMassTransitService(Configuration, builder);
            
            builder.Populate(services);
            Provider = new AutofacServiceProvider(builder.Build());
            return Provider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.ConfigSwaggerService();
            app.UseSignalR(routes =>
            {
                routes.MapHub<AppHub>("/AppHub");
            });

        }
    }
}
