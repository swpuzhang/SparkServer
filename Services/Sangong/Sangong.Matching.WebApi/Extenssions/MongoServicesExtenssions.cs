using Commons.Infrastruct;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;

namespace Sangong.Matching.WebApi.Extenssions
{
    public static class MongoServicesExtenssions
    {
        public static void AddMongoServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoSettings>(
                configuration.GetSection(nameof(MongoSettings)));

            services.AddSingleton<IMongoSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoSettings>>().Value);
        }
    }
}
