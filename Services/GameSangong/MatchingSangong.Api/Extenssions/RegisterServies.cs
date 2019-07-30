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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatchingSangong.Api.Extenssions
{
    public static class RegisterServies
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            services.AddScoped<ISampleAppService, SampleAppService>();
            services.AddScoped<ISampleInfoRepository, SampleInfoRepository>();
            services.AddScoped<SampleContext>();

            services.AddScoped<IUserIdGenRepository, UserIdGenRepository>();
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<LoginCommand, HasBodyResponse<SampleResponse>>, SampleCommandHandler>();
           

        }
    }
}
