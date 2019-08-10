using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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
        public static string mqConnectionStr = null;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddAutoMapperSetup();
            services.RegisterSwaggerServices();
            services.AddServices(Configuration);
            ContainerBuilder builder = new ContainerBuilder();
            services.AddMassTransitServices(Configuration, builder);
            builder.Populate(services);
            return new AutofacServiceProvider(builder.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {

            app.ConfigSwaggerServices();
            app.ConfigServices(Configuration);

            app.ConfigSwaggerServices();
            app.UseSignalR(routes =>
            {
                routes.MapHub<AppHub>("/AppHub");
            });

        }
    }
}
