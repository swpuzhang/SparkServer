using MsgCenter.Application.Services;
using MsgCenter.Domain.CommandHandlers;
using MsgCenter.Domain.Commands;
using MsgCenter.Domain.Models;
using MsgCenter.Domain.RepositoryInterface;
using MsgCenter.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;

namespace MsgCenter.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IMsgCenterService, MsgCenterService>();

            //存储
            
            services.AddScoped<IMsgCenterRedisRepository, MsgCenterRedisRepository>();

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            //services.AddScoped<IRequestHandler<MsgCenterCommand, BodyResponse<MsgCenterInfo>>, MsgCenterCommandHandler>();

            services.AddMediatR(typeof(Startup));
            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));

            //manager

        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            /*var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());*/
        }
    }
}
