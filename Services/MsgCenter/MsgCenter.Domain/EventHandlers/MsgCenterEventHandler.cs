using AutoMapper;
using MassTransit;
using MediatR;
using MsgCenter.Domain.Events;
using MsgCenter.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MsgCenter.Domain.EventHandlers
{
    public class MsgCenterEventHandler : INotificationHandler<MsgCenterEvent>
    {
        private readonly IMsgCenterRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public MsgCenterEventHandler(
            IMsgCenterRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(MsgCenterEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
