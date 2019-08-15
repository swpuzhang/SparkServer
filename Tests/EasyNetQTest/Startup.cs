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
using EasyNetQ;
using Microsoft.Extensions.Hosting;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Commons.Domain.Models;

namespace EasyNetQTest
{

    public class MyConventions : Conventions
    {
        public MyConventions(ITypeNameSerializer typeNameSerializer) : base(typeNameSerializer)
        {
            ErrorQueueNamingConvention = messageInfo => "MyErrorQueue";
            ExchangeNamingConvention = (msg) =>
            {
                if (msg.Equals(typeof(AppRoomRequest)))
                {

                }
                return "";
            };
        }
    }

        public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            ContainerBuilder builder = new ContainerBuilder();
            services.AddSingleton<IHostedService, BusService>();
            services.AddScoped<IService, Service>();
            builder.RegisterEasyNetQ("host=localhost;virtualHost=SkyWatch;username=SkyWatch;password=sky_watch_2019_best", 
                c => 
                {
                   
                });
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

            app.UseMvc();
        }
    }
}
