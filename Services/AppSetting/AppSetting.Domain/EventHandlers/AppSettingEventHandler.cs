using AutoMapper;
using MassTransit;
using MediatR;
using AppSetting.Domain.Events;
using AppSetting.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AppSetting.Domain.EventHandlers
{
    public class AppSettingEventHandler : INotificationHandler<AppSettingEvent>
    {
        private readonly IFadebackInfoRepository _accountRepository;
        private readonly IAppSettingRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public AppSettingEventHandler(IFadebackInfoRepository rep,
            IAppSettingRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _accountRepository = rep;
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(AppSettingEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
