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
using Commons.Extenssions;
using Dummy.Domain.Manager;
using Commons.IntegrationBus;
using Commons.Domain.Managers;

namespace Dummy.Game.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IDummyAppService, DummyAppService>();
            services.AddScoped<IDummyGameService, DummyGameService>();
            
            //存储

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<DummyCommand, BodyResponse<DummyInfo>>, DummyCommandHandler>();
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
