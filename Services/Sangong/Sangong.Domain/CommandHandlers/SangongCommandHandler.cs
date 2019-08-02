using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
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

namespace Sangong.Domain.CommandHandlers
{
    public class SangongCommandHandler :
        IRequestHandler<SangongCommand, BodyResponse<SangongInfo>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private IRequestClient<GetMoneyMqCommand> _moneyClient;

        private readonly RedisHelper _redis;
        public SangongCommandHandler(RedisHelper redis, IMediatorHandler bus, 
            IRequestClient<GetMoneyMqCommand> moneyClient)
        {
            _redis = redis;
            _bus = bus;
            _moneyClient = moneyClient;
        }
        public Task<BodyResponse<SangongInfo>> Handle(SangongCommand request, CancellationToken cancellationToken)
        {
            BodyResponse<SangongInfo> response = new BodyResponse<SangongInfo>(StatuCodeDefines.LoginError, null, null);
            return Task.FromResult(response);
        }
    }
}
