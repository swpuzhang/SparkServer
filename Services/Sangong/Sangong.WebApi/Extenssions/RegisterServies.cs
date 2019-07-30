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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sangong.WebApi.Extenssions
{
    public static class RegisterServies
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<ISangongAppService, SangongAppService>();
            services.AddScoped<ISangongInfoRepository, SangongInfoRepository>();
            services.AddScoped<SangongContext>();

            services.AddScoped<IUserIdGenRepository, UserIdGenRepository>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<LoginCommand, HasBodyResponse<SangongResponse>>, SangongCommandHandler>();
           

        }
    }
}
