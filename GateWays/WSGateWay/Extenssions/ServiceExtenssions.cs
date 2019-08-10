using Autofac;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WSGateWay.Hubs;
using WSGateWay.Manager;
using WSGateWay.Services;

namespace WSGateWay.Extenssions
{
    public static class ServiceExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<UserConnManager>(new UserConnManager());
            services.AddSingleton<IRpcCaller<AppHub>, RpcCaller<AppHub>>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ICommonService, CommonService>();
        }
        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            //var container = app.ApplicationServices;
            
        }
    }
}
