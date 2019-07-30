using Account.Domain.Events;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Extenssions;
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
    public class LoginEventHandler : INotificationHandler<LoginEvent>
    {
        private readonly IAccountInfoRepository _accountRepository;
        private readonly IUserIdGenRepository _genRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public LoginEventHandler(IAccountInfoRepository rep,
            IUserIdGenRepository genRepository,
            IAccountRedisRepository redis,
            IBusControl mqBus, IMapper mapper)
        {
            _accountRepository = rep;
            _genRepository = genRepository;
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
    }
}
