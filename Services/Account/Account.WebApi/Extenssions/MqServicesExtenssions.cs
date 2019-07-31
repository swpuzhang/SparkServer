using Account.Application.Services;
using Account.Domain;
using Account.Domain.RepositoryInterface;
using Autofac;
using Commons.MqCommands;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace Account.WebApi.Extenssions
{
    public static class MqServicesExtenssions
    {
        public static void AddMassTransitServices(this IServiceCollection services, IConfiguration Configuration, 
            ContainerBuilder builder)
        {
            services.AddSingleton<IHostedService, HostedService>();
            builder.AddMassTransit(x =>
            {
                var rabbitCfg = Configuration.GetSection("rabbitmq");
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseSerilog();
                   
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
                var moneyMqUrl = rabbitCfg["Money"];
                x.AddRequestClient<GetMoneyMqCommand>(new Uri(moneyMqUrl));
            });
        }


       
       

       
    }
}
