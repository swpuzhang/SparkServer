using Friend.Application.Services;
using Friend.Domain.CommandHandlers;
using Friend.Domain.Commands;
using Friend.Domain.Models;
using Friend.Domain.RepositoryInterface;
using Friend.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;

namespace Friend.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IFriendService, FriendService>();

            //存储
            services.AddScoped<IFriendInfoRepository, FriendInfoRepository>();
            services.AddScoped<IFriendRedisRepository, FriendRedisRepository>();
            services.AddScoped<FriendContext>();

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<ApplyAddFriendCommand, BodyResponse<NullBody>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<AgreeAddFriendCommand, BodyResponse<NullBody>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<GetFriendsCommand, BodyResponse<FriendVM>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<GetApplysCommand, BodyResponse<FriendVM>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<IgnoreApplyCommand, BodyResponse<NullBody>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<UploadPlatformFriendsCommand, BodyResponse<NullBody>>, FriendCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteFriendCommand, BodyResponse<NullBody>>, FriendCommandHandler>();
            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));

            //manager

        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            /*var container = app.ApplicationServices;
            var levelManager = container.GetRequiredService<LevelManager>();
            levelManager.LoadConfig(container.GetRequiredService<ILevelConfigRepository>());*/
        }
    }
}
