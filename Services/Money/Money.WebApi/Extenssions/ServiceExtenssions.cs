using Money.Application.Services;
using Money.Domain;
using Money.Domain.RepositoryInterface;
using Money.Infrastruct;
using Autofac;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Commons.Domain.Bus;
using MediatR;
using Money.Domain.Commands;
using Money.Domain.Models;
using Money.Domain.CommandHandlers;
using Commons.Extenssions;

namespace Money.WebApi.Extenssions
{
    public static class ServiceExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMoneyService, MoneyService>();
            services.AddScoped<IMoneyInfoRepository, MoneyInfoRepository>();
            services.AddScoped<IMoneyRedisRepository, MoneyRedisRepository>();
            services.AddScoped<MoneyContext>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<GetMoneyCommand, HasBodyResponse<MoneyInfo>>, GetMoneyCommandHandler>();
            services.AddSingleton<RedisHelper>(new RedisHelper(configuration["redis:ConnectionString"]));
            services.AddMediatR(typeof(Startup));

        }
    }
}
