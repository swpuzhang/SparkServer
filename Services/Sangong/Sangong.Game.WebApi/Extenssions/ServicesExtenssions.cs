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

namespace Sangong.Game.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<ISangongAppService, SangongAppService>();
            services.AddScoped<ISangongInfoRepository, SangongInfoRepository>();
            services.AddScoped<SangongContext>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<SangongCommand, BodyResponse<SangongInfo>>, SangongCommandHandler>();
            services.AddMediatR(typeof(Startup));


        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            /*var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());*/
        }
    }
}
