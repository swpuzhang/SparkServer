using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Commons.Extenssions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using MassTransit.SerilogIntegration;

namespace MatchingSangong.Api
{
    public class Program
    {
        public static readonly string Namespace = typeof(Program).Namespace;
        public static readonly string AppName = Namespace.Substring(Namespace.LastIndexOf('.', Namespace.LastIndexOf('.') - 1) + 1);

        public static void Main(string[] args)
        {
            var config = GetConfiguration(args);
            Log.Logger = CreateSerilogLogger(config);

            Log.Information("CreateWebHostBuilder ({ApplicationContext})...", "Sample");
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseStartup<Startup>();

        private static IConfiguration GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
            return builder.Build();
        }

        private static Serilog.ILogger CreateSerilogLogger(IConfiguration configuration)
        {

            /*var writeTos = configuration.GetSection("Serilog:WriteTo");
            foreach(var oneWrite in writeTos.GetChildren())
            {
                oneWrite["Args:path"] = $"log-{DateTime.Now.ToNormal()}.txt";
            }*/
            return new LoggerConfiguration()
                .MinimumLevel.Information()
                //.Enrich.WithProperty("ApplicationContext", AppName)
                //.Enrich.FromLogContext()
                .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Error)
                //.WriteTo.File($"log-{DateTime.Now.ToNormal()}.txt", rollOnFileSizeLimit:true, fileSizeLimitBytes:1024*1024*100)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
