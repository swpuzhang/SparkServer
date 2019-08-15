using Commons.Extenssions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.DotNet.PlatformAbstractions;
using System;
using System.IO;

namespace ApiGateWay
{
    public class Program
    {
        public static readonly string AppName = typeof(Program).Namespace;
       
        public static void Main(string[] args)
        {
            var config = GetConfiguration(args);
            Log.Logger = LogConfig.CreateSerilogLogger(config, AppName);

            Log.Information("CreateWebHostBuilder ({ApplicationContext})...", "Account");
            Log.Information($"path:{ApplicationEnvironment.ApplicationBasePath}");
            CreateWebHostBuilder(args, config).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args, IConfiguration configuratioin) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(configuratioin)
                .UseSerilog()
                .UseStartup<Startup>();

        private static IConfiguration GetConfiguration(string[] args)
        {
            bool isEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";
            string basePath = isEnvironment ? Directory.GetCurrentDirectory() : ApplicationEnvironment.ApplicationBasePath;
            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
            return builder.Build();
        }

        
    }

    
}
