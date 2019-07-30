using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MassTransit;
using Microsoft.Extensions.Hosting;
using Autofac;

using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Autofac.Extensions.DependencyInjection;
using TestMessage;

namespace MassTransitTest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            /*services.AddScoped<DoSomethingConsumer>();

            services.AddMassTransit(x =>
            {
                // add the consumer to the container
                x.AddConsumer<DoSomethingConsumer>();
            });

            services.AddSingleton(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host("localhost", "/", h => { });
                cfg.ReceiveEndpoint(host, "web-service-endpoint", e =>
                {
                    

                    e.Consumer<DoSomethingConsumer>(provider);


                    //EndpointConvention.Map<DoSomething>(e.InputAddress);

                });
            }));

            services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());
            services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());

            services.AddScoped(provider => provider.GetRequiredService<IBus>().CreateRequestClient<DoSomething>());

            services.AddSingleton<IHostedService, BusService>();*/


            /*services.AddMassTransit(x =>
            {
                x.AddConsumer<DoSomethingConsumer>();

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost/SkyWatch"), h =>
                    {
                        h.Username("SkyWatch");
                        h.Password("sky_watch_2019_best");

                    });

                    cfg.ReceiveEndpoint(host, "web-service-endpoint", e =>
                    {
                       

                        e.ConfigureConsumer<DoSomethingConsumer>(provider);

                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));

                x.AddRequestClient<DoSomething>();
            });
            services.AddSingleton<IHostedService, BusService>();*/


            /*ContainerBuilder builder = new ContainerBuilder();
            services.AddSingleton<IHostedService, BusService>();
            builder.Populate(services);
            builder.AddMassTransit(x =>
            {
                x.AddConsumer<DoSomethingConsumer>();
                x.AddBus(context =>
                {
                    return Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var host = cfg.Host(new Uri("rabbitmq://localhost/SkyWatch"), h =>
                        {
                            h.Username("SkyWatch");
                            h.Password("sky_watch_2019_best");

                        });

                        cfg.ReceiveEndpoint(host,"web-service-endpoint", ec =>
                        {

                            //ec.ConfigureConsumers(context);
                            ec.Consumer<DoSomethingConsumer>();

                        });

                        // or, configure the endpoints by convention
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });*/
            ContainerBuilder builder = new ContainerBuilder();
            services.AddSingleton<IHostedService, BusService>();
            services.AddScoped<IService, Service>();
            services.AddScoped<IService2, Service2>();
            builder.Populate(services);
            /*builder.AddMassTransit(x =>
             {
                 x.AddConsumers(Assembly.GetExecutingAssembly());
                 //x.AddConsumer<DoSomethingConsumer>();
                 x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                 {
                     var host = cfg.Host(new Uri("rabbitmq://localhost/Test2"), h =>
                     {
                         h.Username("SkyWatch");
                         h.Password("sky_watch_2019_best");

                     });

                     cfg.ReceiveEndpoint("test_queue", ec =>
                     {

                         ec.ConfigureConsumers(context);
                         //ec.Consumer(typeof(DoSomethingConsumer),  c => Activator.CreateInstance(c));
                         //ec.Consumer<DoSomethingConsumer>();
                     });

                     // or, configure the endpoints by convention
                     //cfg.ConfigureEndpoints(context);

                 }));
                 
                 x.AddRequestClient<DoSomething>(new Uri("rabbitmq://localhost/Test3/test_queue"));
             });*/

            builder.AddMassTransit(x =>
            {
                x.AddConsumers(Assembly.GetExecutingAssembly());

                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host(new Uri("rabbitmq://localhost/SkyWatch"), h =>
                    {
                        h.Username("SkyWatch");
                        h.Password("sky_watch_2019_best");
                    });

                    cfg.ReceiveEndpoint("MassTestQueue", ec =>
                    {
                        ec.ConfigureConsumers(context);
                    });
                }));
                x.AddRequestClient<DoSomething>();// (new Uri("rabbitmq://localhost/Test3/MassTestQueue"));
                
            });
            Provider = new AutofacServiceProvider(builder.Build());
            
            return Provider;

        }

        public static IServiceProvider Provider { get; private set; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
