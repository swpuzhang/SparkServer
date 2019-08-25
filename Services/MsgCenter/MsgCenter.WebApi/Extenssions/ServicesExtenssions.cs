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
using System.Collections.Generic;

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
            services.AddScoped<IRequestHandler<GetUserMsgsCommand, BodyResponse<UserMsgs>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<ReadedCommand, BodyResponse<NullBody>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<RecieveMsgReward, BodyResponse<NullBody>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteMsgCommand, BodyResponse<NullBody>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<ReadedAllCommand, BodyResponse<NullBody>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<RecieveAllMsgRewardCommand, BodyResponse<List<RewardInfo>>>, MsgCenterCommandHandler>();
            services.AddScoped<IRequestHandler<PushMsgCommand, BodyResponse<NullBody>>, MsgCenterCommandHandler>();
           

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
