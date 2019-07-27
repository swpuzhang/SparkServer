using Sample.Domain.Commands;
using Sample.Domain.Models;
using Sample.Domain.RepositoryInterface;
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

namespace Sample.Domain.CommandHandlers
{
    public class SampleCommandHandler :
        IRequestHandler<LoginCommand, HasBodyResponse<SampleResponse>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly ISampleInfoRepository _sampleRepository;
        private readonly  IUserIdGenRepository _genRepository;
        private readonly RedisHelper _redis;
        public SampleCommandHandler(ISampleInfoRepository rep, IUserIdGenRepository genRepository, RedisHelper redis, IMediatorHandler bus)
        {
            _sampleRepository = rep;
            _genRepository = genRepository;
            _redis = redis;
            _bus = bus;
        }
        public Task<HasBodyResponse<SampleResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            
            HasBodyResponse<SampleResponse> response = new HasBodyResponse<SampleResponse>(StatuCodeDefines.LoginError, null, null);
            return Task.FromResult(response);

        }
    }
}
