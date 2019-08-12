using AutoMapper;
using MassTransit;
using MediatR;
using Sangong.Domain.Events;
using Sangong.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sangong.Domain.EventHandlers
{
    public class SangongEventHandler : INotificationHandler<SangongEvent>
    {
        private readonly ISangongRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public SangongEventHandler(
            ISangongRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(SangongEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
