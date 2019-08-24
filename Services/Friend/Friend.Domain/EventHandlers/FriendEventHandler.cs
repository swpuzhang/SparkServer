using AutoMapper;
using MassTransit;
using MediatR;
using Friend.Domain.Events;
using Friend.Domain.RepositoryInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Friend.Domain.EventHandlers
{
    public class FriendEventHandler : INotificationHandler<FriendEvent>
    {
        private readonly IFriendInfoRepository _accountRepository;
        private readonly IFriendRedisRepository _redis;
        private readonly IBusControl _mqBus;
        private readonly IMapper _mapper;
        public FriendEventHandler(IFriendInfoRepository rep,
            IFriendRedisRepository redis,
            IBusControl mqBus,
            IMapper mapper)
        {
            _accountRepository = rep;
            _redis = redis;
            _mqBus = mqBus;
            _mapper = mapper;
        }
        public Task Handle(FriendEvent notification, CancellationToken cancellationToken)
        {
            
            return Task.CompletedTask;
        }
    }
}
