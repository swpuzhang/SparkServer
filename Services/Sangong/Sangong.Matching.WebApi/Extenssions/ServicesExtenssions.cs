using Sangong.Application.Services;
using Sangong.Domain.CommandHandlers;
using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using Sangong.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Sangong.Domain.Manager;
using Commons.Extenssions;
using Sangong.MqCommands;

namespace Sangong.Matching.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //数据存储
            services.AddScoped<ISangongRedisRepository, SangongRedisRepository>();
            services.AddScoped<ISangongInfoRepository, SangongInfoRepository>();
            services.AddSingleton<IConfigRepository, ConfigRepository>();
            services.AddScoped<SangongContext>();

            //服务
            services.AddScoped<ISangongAppService, SangongAppService>();
            services.AddScoped<ISangongMatchingService, SangongMatchingService>();

            //命令总线
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<SangongCommand, BodyResponse<SangongInfo>>, SangongCommandHandler>();
            services.AddScoped<IRequestHandler<SangongPlaynowCommand, BodyResponse<SangongMatchingResponseInfo>>, SangongMatchingCommandHandler>();

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
