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
using GameSample.Domain.Manager;
using Commons.Extenssions;
using GameSample.MqCommands;

namespace GameSample.Matching.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //数据存储
            services.AddScoped<IGameSampleRedisRepository, GameSampleRedisRepository>();
            services.AddSingleton<IConfigRepository, ConfigRepository>();
            services.AddSingleton<IRoomListConfigRepository, RoomListConfigRepository>();
            

            //服务
            services.AddScoped<IGameSampleAppService, GameSampleAppService>();
            services.AddScoped<IGameSampleMatchingService, GameSampleMatchingService>();

            //命令总线
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<GameSampleCommand, BodyResponse<GameSampleInfo>>, GameSampleCommandHandler>();
            services.AddScoped<IRequestHandler<GameSamplePlaynowCommand, BodyResponse<GameSampleMatchingResponseInfo>>, GameSampleMatchingCommandHandler>();
            services.AddScoped<IRequestHandler<BlindMatchingCommand, BodyResponse<GameSampleMatchingResponseInfo>>, GameSampleMatchingCommandHandler>();

            
            services.AddMediatR(typeof(Startup));
            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));
            //注册MANAGER
            services.AddSingleton<MatchingManager>();
            services.AddSingleton<RoomManager>();
            
        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            /*var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());*/
        }
    }
}
