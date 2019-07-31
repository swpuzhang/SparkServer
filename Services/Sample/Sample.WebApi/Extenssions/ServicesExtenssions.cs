using Sample.Application.Services;
using Sample.Domain.CommandHandlers;
using Sample.Domain.Commands;
using Sample.Domain.Models;
using Sample.Domain.RepositoryInterface;
using Sample.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Sample.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services)
        {

            services.AddScoped<ISampleAppService, SampleAppService>();
            services.AddScoped<ISampleInfoRepository, SampleInfoRepository>();
            services.AddScoped<SampleContext>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<SampleCommand, HasBodyResponse<SampleInfo>>, SampleCommandHandler>();
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
