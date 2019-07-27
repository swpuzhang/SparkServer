using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WSGateWay.Hubs;
using WSGateWay.Manager;
using WSGateWay.Services;

namespace WSGateWay.Extenssions
{
    public static class RegisterServies
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<UserConnManager>(new UserConnManager());
            services.AddSingleton<IRpcCaller<AppHub>, RpcCaller<AppHub>>();
            services.AddScoped<ICommandService, CommandService>();
            services.AddScoped<ICommonService, CommonService>();
        }
    }
}
