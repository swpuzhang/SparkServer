﻿using Reward.Application.Services;
using Reward.Domain.CommandHandlers;
using Reward.Domain.Commands;
using Reward.Domain.Models;
using Reward.Domain.RepositoryInterface;
using Reward.Infrastruct;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Domain.Models;
using Commons.Infrastruct;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Commons.Extenssions;
using Reward.Domain.Events;
using Reward.Domain.EventHandlers;
using System.Collections.Generic;

namespace Reward.WebApi.Extenssions
{
    public static class ServicesExtenssions
    {
        public static void AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            //服务
            services.AddScoped<IRewardService, RewardService>();
            services.AddScoped<IActivityService, ActivityService>();
            //存储
            services.AddTransient<IRegisterRewardConfigRepository, RegisterRewardConfigRepository>();
            services.AddScoped<IRegisterRewardRepository, RegisterRewardRepository>();
            services.AddTransient<ILoginRewardConfigRepository, LoginRewardConfigRepository>();
            services.AddTransient<IBankruptcyConfigRepository, BankruptcyConfigRepository>();
            services.AddSingleton<IRewardRedisRepository, RewardRedisRepository>();
            services.AddTransient<IInviteRewardConfigRepository, InviteRewardConfigRepository>();
            services.AddTransient<IGameActivityConfigRepository, GameActivityConfigRepository>();
            services.AddSingleton<IActivityRedisRepository, ActivityRedisRepository>();

            //命令
            services.AddScoped<IMediatorHandler, InProcessBus>();
            services.AddScoped<IRequestHandler<GetRegisterRewardCommand, BodyResponse<RewardInfoVM>>, RewardCommandHandler>();
            services.AddScoped<IRequestHandler<QueryRegisterRewardCommand, BodyResponse<RegisterRewardVM>>, RewardCommandHandler>();
            services.AddScoped<IRequestHandler<GetLoginRewardCommand, BodyResponse<RewardInfoVM>>, LoginRewardCommandHandler>();
            services.AddScoped<IRequestHandler<QueryLoginRewardCommand, BodyResponse<LoginRewardVM>>, LoginRewardCommandHandler>();
            services.AddScoped<IRequestHandler<QueryBankruptcyCommand, BodyResponse<BankruptcyInfoVM>>, BankruptcyCommandHandler>();
            services.AddScoped<IRequestHandler<GetBankruptcyRewardCommand, BodyResponse<RewardInfoVM>>, BankruptcyCommandHandler>();
            services.AddScoped<IRequestHandler<GetBankruptcyRewardCommand, BodyResponse<RewardInfoVM>>, BankruptcyCommandHandler>();
            services.AddScoped<INotificationHandler<InvitedFriendEvent>, RewardEventHandler>();
            services.AddScoped<INotificationHandler<InvitedFriendRegisterdEvent>, RewardEventHandler>();
            services.AddScoped<IRequestHandler<GameActivityCommand, List<OneGameActivityInfoVM>>, GameActivityCmdHandler>();
            services.AddScoped<IRequestHandler<GetGameActRewardCommand, BodyResponse<RewardInfoVM>>, GameActivityCmdHandler>();

            services.AddMediatR(typeof(Startup));
            services.AddSingleton(new RedisHelper(configuration["redis:ConnectionString"]));

            //manager
            services.AddSingleton<RegisterRewardConfig>();
            services.AddSingleton<LoginRewardConfig>();
            services.AddSingleton<BankruptcyConfig>();
            services.AddSingleton<InviteRewardConfig>();
            services.AddSingleton<AllGameActivityConfig>();
        }

        public static void ConfigServices(this IApplicationBuilder app, IConfiguration configuration)
        {

            var container = app.ApplicationServices;
            var registerRep = container.GetRequiredService<IRegisterRewardConfigRepository>();
            var loginRep = container.GetRequiredService<ILoginRewardConfigRepository>();
            var bankruptcyRep = container.GetRequiredService<IBankruptcyConfigRepository>();
            var registerConfig = registerRep.LoadConfig();
            var loginConfig = loginRep.LoadConfig();
            var bankruptcyConfig = bankruptcyRep.LoadConfig();
            container.GetRequiredService<RegisterRewardConfig>().DayRewards = registerConfig.DayRewards;
            container.GetRequiredService<LoginRewardConfig>().DayRewards = loginConfig.DayRewards;
            container.GetRequiredService<BankruptcyConfig>().BankruptcyRewards = bankruptcyConfig.BankruptcyRewards;
            var inviteRep = container.GetRequiredService<IInviteRewardConfigRepository>();
            var inviteConfig = inviteRep.LoadConfig();
            container.GetRequiredService<InviteRewardConfig>().InviteRewards = inviteConfig.InviteRewards;
            var gameActRep = container.GetRequiredService<IGameActivityConfigRepository>();
            var gameActConfig = gameActRep.LoadConfig();
            container.GetRequiredService<AllGameActivityConfig>().AllGameConfigs = gameActConfig;
        }
    }
}
