using AutoMapper;
using MassTransit;
using MediatR;
using GameSample.Domain.Events;
using GameSample.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GameSample.Domain.EventHandlers
{
    public class GameSampleEventHandler : INotificationHandler<GameSampleEvent>
    {
        private readonly IGameSampleRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public GameSampleEventHandler(
            IGameSampleRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(GameSampleEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
