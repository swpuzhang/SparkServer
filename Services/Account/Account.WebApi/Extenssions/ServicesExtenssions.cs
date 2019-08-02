using Account.Application.Services;
using Account.Domain.CommandHandlers;
using Account.Domain.Commands;
using Account.Domain.EventHandlers;
using Account.Domain.Events;
using Account.Domain.Manager;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Account.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Extenssions;
using Commons.Infrastruct;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Account.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IAccountInfoRepository, AccountInfoRepository>();
            services.AddScoped<AccountContext>();
            services.AddScoped<IUserIdGenRepository, UserIdGenRepository>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<LoginCommand,
                BodyResponse<AccountResponse>>, AccountCommandHandler>();
            services.AddScoped<IRequestHandler<GetGameInfoCommand, BodyResponse<GameInfo>>,
                GetGameInfoCommandHandler>();
            services.AddScoped<IRequestHandler<GetLevelInfoCommand, BodyResponse<LevelInfo>>,
                GetLevelInfoCommandHandler>();
            services.AddScoped<IRequestHandler<GetSelfAccountCommand, BodyResponse<AccountDetail>>,
                GetSelfAccountCommandHandler>();
            services.AddScoped<INotificationHandler<LoginEvent>,
                LoginEventHandler>();
            services.AddScoped<IAccountRedisRepository, AccountRedisRepository>();
            services.AddScoped<IGameInfoRepository, GameInfoRepository>();
            services.AddScoped<ILevelInfoRepository, LevelInfoRepository>();
            services.AddMediatR(typeof(Startup));
            services.AddTransient<ILevelConfigRepository, LevelConfigRepository>();
            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));
            services.AddSingleton<LevelManager>();
        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());
        }
    }
}
