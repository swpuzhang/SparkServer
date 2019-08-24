using ServerLog.Domain.Commands;
using ServerLog.Domain.Models;
using ServerLog.Domain.RepositoryInterface;
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

namespace ServerLog.Domain.CommandHandlers
{
    /*public class ServerLogCommandHandler :
        IRequestHandler<ServerLogCommand, BodyResponse<GameLogInfo>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IGameLogInfoRepository _serverlogRepository;
        private readonly IServerLogRedisRepository _redis;
        public ServerLogCommandHandler(IGameLogInfoRepository rep, IServerLogRedisRepository redis, IMediatorHandler bus)
        {
            _serverlogRepository = rep;
            _redis = redis;
            _bus = bus;
        }
        public Task<BodyResponse<ServerLogInfo>> Handle(ServerLogCommand request, CancellationToken cancellationToken)
        {
            
            BodyResponse<ServerLogInfo> response = new BodyResponse<ServerLogInfo>(StatusCodeDefines.LoginError, null, null);
            return Task.FromResult(response);

        }
    }*/
}
