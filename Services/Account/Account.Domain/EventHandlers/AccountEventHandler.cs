using Account.Domain.Events;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Extenssions;
using Commons.MqCommands;
using Commons.MqEvents;
using MassTransit;
using MediatR;
using Serilog;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Account.Domain.EventHandlers
{
    public class AccountEventHandler : INotificationHandler<LoginEvent>,
        INotificationHandler<FinishRegisterRewardEvent>
    {
        private readonly IAccountInfoRepository _accountRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public AccountEventHandler(IAccountInfoRepository rep,
            IAccountRedisRepository redis,
            IBusControl mqBus, 
            IMapper mapper)
        {
            _accountRepository = rep;
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(LoginEvent notification, CancellationToken cancellationToken)
        {
            //如果登录信息有更新, 那么更新数据库
            if (notification.IsNeedUpdate)
            {
                 _accountRepository.Update(notification.Info);
            }
            try
            {
                if (notification.IsRegister)
                {
                    //通知注册
                    _ = _mqBus.Publish<RegistMqEvent>(_mapper.Map<RegistMqEvent>(notification.AccounResponse));
                }
                else
                {
                    //通知登录
                    _ = _mqBus.Publish<LoginMqEvent>(_mapper.Map<LoginMqEvent>(notification.AccounResponse));
                }
            }
            catch (Exception ex)
            {
                Log.Logger.Error($"Publish LoginMqEvent exception {ex.Message}");
            }

            //设置登录信息
             _redis.SetLoginCheckInfo(notification.Info.PlatformAccount, notification.Info);
            //设置玩家信息缓存
            _redis.SetAccountInfo(notification.Info);
            return Task.CompletedTask;
        }

        public async Task Handle(FinishRegisterRewardEvent notification, CancellationToken cancellationToken)
        {
            using (var loker = _redis.Loker(KeyGenHelper.GenUserKey(notification.Id, AccountInfo.className)))
            {
                loker.Lock();
                AccountInfo info = await _redis.GetAccountInfo(notification.Id);
                if (info == null)
                {
                    info = await _accountRepository.GetByIdAsync(notification.Id);
                }
                info.Flags |= GetAccountBaseInfoMqResponse.SomeFlags.RegisterReward;
                await Task.WhenAll(_accountRepository.UpdateAsync(info), _redis.SetAccountInfo(info));
            }
        }
        
    }
}
