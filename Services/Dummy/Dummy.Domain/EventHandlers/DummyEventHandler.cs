using AutoMapper;
using MassTransit;
using MediatR;
using Dummy.Domain.Events;
using Dummy.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dummy.Domain.EventHandlers
{
    public class DummyEventHandler : INotificationHandler<DummyEvent>
    {
        private readonly IDummyRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public DummyEventHandler(
            IDummyRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(DummyEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
