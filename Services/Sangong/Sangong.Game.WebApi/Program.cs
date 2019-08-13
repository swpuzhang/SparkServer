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
using Microsoft.DotNet.PlatformAbstractions;

namespace Sangong.Game.WebApi
{
    public class Program
    {
        public static readonly string AppName = typeof(Program).Namespace;

        public static void Main(string[] args)
        {
            var config = GetConfiguration(args);
            Log.Logger = LogConfig.CreateSerilogLogger(config, AppName);

            Log.Information("CreateWebHostBuilder ({ApplicationContext})...", "Account");
            CreateWebHostBuilder(args, config).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration configuratioin) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuratioin)
                .UseSerilog()
                .UseStartup<Startup>();

        private static IConfiguration GetConfiguration(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(ApplicationEnvironment.ApplicationBasePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
            return builder.Build();
        }
    }
}
