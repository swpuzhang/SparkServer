using GameSample.Application.Services;
using GameSample.Domain.CommandHandlers;
using GameSample.Domain.Commands;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
using GameSample.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;
using GameSample.Domain.Manager;
using Commons.IntegrationBus;
using Commons.Domain.Managers;

namespace GameSample.Game.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IGameSampleAppService, GameSampleAppService>();
            services.AddScoped<IGameSampleGameService, GameSampleGameService>();
            
            //存储

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<GameSampleCommand, BodyResponse<GameSampleInfo>>, GameSampleCommandHandler>();
            services.AddMediatR(typeof(Startup));

            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));

            //manager
            services.AddSingleton<GameRoomManager>();
            services.AddSingleton<MqManager>();
            
        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            /*var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());*/
        }
    }
}
