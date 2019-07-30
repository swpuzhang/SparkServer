using Sample.Application.Services;
using Sample.Domain;
using Sample.Domain.RepositoryInterface;
using Sample.Infrastruct;
using Autofac;
using Commons.Domain.Models;
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

namespace MatchingSangong.Api.Extenssions
{
    public static class ServiceExtenssions
    {
        public static void AddMassTransitService(this IServiceCollection services, IConfiguration Configuration, 
            ContainerBuilder builder)
        {
            services.AddSingleton<IHostedService, HostedService>();
            builder.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseSerilog();
                    var rabbitCfg = Configuration.GetSection("rabbitmq");
                    var host = cfg.Host(rabbitCfg["host"], rabbitCfg["vhost"], h =>
                    {
                        h.Username(rabbitCfg["username"]);
                        h.Password(rabbitCfg["passwd"]);

                    });

                    cfg.ReceiveEndpoint(rabbitCfg["queue"], ec =>
                    {

                        ec.ConfigureConsumers(context);
                        //ec.Consumer(typeof(DoSomethingConsumer), c => Activator.CreateInstance(c));
                        //特殊消息
                        //EndpointConvention.Map<DoSomething>(e.InputAddress);
                    });

                    //cfg.ConfigureEndpoints(context);


                }));

                //添加RequestClient
                //x.AddRequestClient<DoSomething>();
            });
        }


        public static void AddMongoService(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(
                configuration.GetSection(nameof(MongoSettings)));

            services.AddSingleton<IMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoSettings>>().Value);
        }
       

       
    }
}
