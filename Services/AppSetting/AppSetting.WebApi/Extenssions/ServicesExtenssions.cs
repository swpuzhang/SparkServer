using AppSetting.Application.Services;
using AppSetting.Domain.CommandHandlers;
using AppSetting.Domain.Commands;
using AppSetting.Domain.Models;
using AppSetting.Domain.RepositoryInterface;
using AppSetting.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;

namespace AppSetting.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IAppSettingService, AppSettingService>();

            //存储
            services.AddScoped<IFadebackInfoRepository, FadebackInfoRepository>();
            services.AddScoped<IAppSettingRedisRepository, AppSettingRedisRepository>();

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            //services.AddScoped<IRequestHandler<AppSettingCommand, BodyResponse<AppSettingInfo>>, AppSettingCommandHandler>();

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
