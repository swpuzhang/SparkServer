using Dummy.Application.Services;
using Dummy.Domain.CommandHandlers;
using Dummy.Domain.Commands;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using Dummy.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Dummy.Domain.Manager;
using Commons.Extenssions;
using Dummy.MqCommands;

namespace Dummy.Matching.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //数据存储
            services.AddScoped<IDummyRedisRepository, DummyRedisRepository>();
            services.AddSingleton<IConfigRepository, ConfigRepository>();
            services.AddSingleton<IRoomListConfigRepository, RoomListConfigRepository>();
            

            //服务
            services.AddScoped<IDummyAppService, DummyAppService>();
            services.AddScoped<IDummyMatchingService, DummyMatchingService>();

            //命令总线
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<DummyCommand, BodyResponse<DummyInfo>>, DummyCommandHandler>();
            services.AddScoped<IRequestHandler<DummyPlaynowCommand, BodyResponse<DummyMatchingResponseInfo>>, DummyMatchingCommandHandler>();
            services.AddScoped<IRequestHandler<BlindMatchingCommand, BodyResponse<DummyMatchingResponseInfo>>, DummyMatchingCommandHandler>();

            
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
