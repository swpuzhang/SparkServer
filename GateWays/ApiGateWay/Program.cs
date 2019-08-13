using Commons.Extenssions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;
using Microsoft.DotNet.PlatformAbstractions;

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
            var builder = new ConfigurationBuilder()
                .SetBasePath(ApplicationEnvironment.ApplicationBasePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile("Ocelot.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(args);
            return builder.Build();
        }

        
    }

    
}
