using Autofac;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Money.WebApi.Extenssions
{
    public static class MqServicesExtenssions
    {
        public static void AddMassTransitServices(this IServiceCollection services, IConfiguration Configuration,
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
    }
}
