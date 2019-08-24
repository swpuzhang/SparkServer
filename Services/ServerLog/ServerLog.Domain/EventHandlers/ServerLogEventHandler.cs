using AutoMapper;
using MassTransit;
using MediatR;
using ServerLog.Domain.Events;
using ServerLog.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerLog.Domain.EventHandlers
{
   /* public class ServerLogEventHandler : INotificationHandler<ServerLogEvent>
    {
        private readonly IGameLogInfoRepository _accountRepository;
        private readonly IServerLogRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public ServerLogEventHandler(IGameLogInfoRepository rep,
            IServerLogRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _accountRepository = rep;
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(ServerLogEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }*/
}
