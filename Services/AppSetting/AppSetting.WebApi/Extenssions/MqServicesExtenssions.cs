
using Autofac;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using AppSetting.WebApi.Extenssions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AppSetting.WebApi.Extenssions
{
    public static class MqServicesExtenssions
    {
        public static void AddMassTransitService(this IServiceCollection services, IConfiguration Configuration, 
            ContainerBuilder builder)
        {
            services.AddSingleton<IHostedService, HostedService>();
            builder.AddMassTransit(x =>
            {
                var rabbitCfg = Configuration.GetSection("Rabbitmq");
                x.AddConsumers(Assembly.GetExecutingAssembly());
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseSerilog();
                    
                    var host = cfg.Host(new Uri(rabbitCfg["Uri"]), h =>
                    {
                        h.Username(rabbitCfg["UserName"]);
                        h.Password(rabbitCfg["Passwd"]);

                    });

                    cfg.ReceiveEndpoint(rabbitCfg["Queue"], ec =>
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
