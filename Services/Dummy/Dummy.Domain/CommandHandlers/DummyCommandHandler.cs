using Dummy.Domain.Commands;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using Commons.Domain.Bus;
using Commons.Domain.CommandHandler;
using Commons.Extenssions.Defines;
using Commons.Domain.Models;
using Commons.Extenssions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Commons.MqCommands;

namespace Dummy.Domain.CommandHandlers
{
    public class DummyCommandHandler :
        IRequestHandler<DummyCommand, BodyResponse<DummyInfo>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private IRequestClient<GetMoneyMqCommand> _moneyClient;

        private readonly RedisHelper _redis;
        public DummyCommandHandler(RedisHelper redis, IMediatorHandler bus, 
            IRequestClient<GetMoneyMqCommand> moneyClient)
        {
            _redis = redis;
            _bus = bus;
            _moneyClient = moneyClient;
        }
        public Task<BodyResponse<DummyInfo>> Handle(DummyCommand request, CancellationToken cancellationToken)
        {
            BodyResponse<DummyInfo> response = new BodyResponse<DummyInfo>(StatusCodeDefines.LoginError, null, null);
            return Task.FromResult(response);
        }
    }
}
