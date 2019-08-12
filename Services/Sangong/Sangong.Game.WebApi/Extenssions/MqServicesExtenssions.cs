﻿
using Autofac;
using Commons.Domain.Models;
using Commons.Infrastruct;
using Commons.MqCommands;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Sangong.Game.WebApi.Extenssions;
using Sangong.MqCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Sangong.Game.WebApi.Extenssions
{
    public static class MqServicesExtenssions
    {
        public static void AddMassTransitService(this IServiceCollection services, IConfiguration Configuration, 
            ContainerBuilder builder)
        {
            services.AddSingleton<IHostedService, HostedService>();
            builder.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());
                var rabbitCfg = Configuration.GetSection("Rabbitmq");
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.UseSerilog();
                   
                    var host = cfg.Host(rabbitCfg["Host"], rabbitCfg["Vhost"], h =>
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
                x.AddRequestClient<GetMoneyMqCommand>(new Uri(rabbitCfg["Money"]));
                x.AddRequestClient<BuyInMqCommand>(new Uri(rabbitCfg["Money"]));
                x.AddRequestClient<GetAccountInfoMqCommand>(new Uri(rabbitCfg["Account"]));
                
                x.AddRequestClient<UserApplySitMqCommand>(new Uri(rabbitCfg["Mathcing"]));
                //添加RequestClient
                //x.AddRequestClient<DoSomething>();
            });
        }


       
       

       
    }
}
