using Account.Application.Services;
using Account.Domain.CommandHandlers;
using Account.Domain.Commands;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using Account.Infrastruct;
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

namespace Account.WebApi.Extenssions
{
    public static class RegisterServies
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<IAccountInfoRepository, AccountInfoRepository>();
            services.AddScoped<AccountContext>();

            services.AddScoped<IUserIdGenRepository, UserIdGenRepository>();
            services.AddScoped<UserIdGenContext>();


            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<LoginCommand, HasBodyResponse<AccountResponse>>, AccountCommandHandler>();
           

        }
    }
}
