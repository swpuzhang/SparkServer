using GameSample.Domain.Commands;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
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

namespace GameSample.Domain.CommandHandlers
{
    public class GameSampleCommandHandler :
        IRequestHandler<GameSampleCommand, BodyResponse<GameSampleInfo>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private IRequestClient<GetMoneyMqCommand> _moneyClient;

        private readonly RedisHelper _redis;
        public GameSampleCommandHandler(RedisHelper redis, IMediatorHandler bus, 
            IRequestClient<GetMoneyMqCommand> moneyClient)
        {
            _redis = redis;
            _bus = bus;
            _moneyClient = moneyClient;
        }
        public Task<BodyResponse<GameSampleInfo>> Handle(GameSampleCommand request, CancellationToken cancellationToken)
        {
            BodyResponse<GameSampleInfo> response = new BodyResponse<GameSampleInfo>(StatusCodeDefines.LoginError, null, null);
            return Task.FromResult(response);
        }
    }
}
