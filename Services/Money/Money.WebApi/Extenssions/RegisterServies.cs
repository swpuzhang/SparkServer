using Money.Application.Services;
using Money.Domain.CommandHandlers;
using Money.Domain.Commands;
using Money.Domain.Models;
using Money.Domain.RepositoryInterface;
using Money.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.WebApi.Extenssions
{
    public static class RegisterServies
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IMoneyAppService, MoneyAppService>();
            services.AddScoped<IMoneyInfoRepository, MoneyInfoRepository>();
            services.AddScoped<MoneyContext>();

            services.AddScoped<IUserIdGenRepository, UserIdGenRepository>();
            services.AddScoped<UserIdGenContext>();


            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<LoginCommand, HasBodyResponse<MoneyResponse>>, MoneyCommandHandler>();
           

        }
    }
}
