using AutoMapper;
using MassTransit;
using MediatR;
using Sample.Domain.Events;
using Sample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Sample.Domain.EventHandlers
{
    public class SampleEventHandler : INotificationHandler<SampleEvent>
    {
        private readonly ISampleInfoRepository _accountRepository;
        private readonly ISampleRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public SampleEventHandler(ISampleInfoRepository rep,
            ISampleRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _accountRepository = rep;
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(SampleEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
