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

namespace Sangong.Domain.CommandHandlers
{
    public class SangongCommandHandler :
        IRequestHandler<LoginCommand, HasBodyResponse<SangongResponse>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly ISangongInfoRepository _sangongRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly RedisHelper _redis;
        public SangongCommandHandler(ISangongInfoRepository rep, IUserIdGenRepository genRepository, RedisHelper redis, IMediatorHandler bus)
        {
            _sangongRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
        }
        public Task<HasBodyResponse<SangongResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            HasBodyResponse<SangongResponse> response = new HasBodyResponse<SangongResponse>(StatuCodeDefines.LoginError, null, null);
            return Task.FromResult(response);

        }
    }
}
