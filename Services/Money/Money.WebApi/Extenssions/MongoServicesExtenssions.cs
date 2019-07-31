using Autofac;
using Commons.Infrastruct;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Money.WebApi.Extenssions
{
    public static class MongoServicesExtenssions
    {
        public static void AddMongoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(
                configuration.GetSection(nameof(MongoSettings)));

            services.AddSingleton<IMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoSettings>>().Value);
        }
    }
}
