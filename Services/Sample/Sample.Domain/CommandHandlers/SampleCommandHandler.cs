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
        IRequestHandler<SampleCommand, BodyResponse<SampleInfo>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly ISampleInfoRepository _sampleRepository;
        private readonly ISampleRedisRepository _redis;
        public SampleCommandHandler(ISampleInfoRepository rep, ISampleRedisRepository redis, IMediatorHandler bus)
        {
            _sampleRepository = rep;
            _redis = redis;
            _bus = bus;
        }
        public Task<BodyResponse<SampleInfo>> Handle(SampleCommand request, CancellationToken cancellationToken)
        {
            
            BodyResponse<SampleInfo> response = new BodyResponse<SampleInfo>(StatusCodeDefines.LoginError, null, null);
            return Task.FromResult(response);

        }
    }
}
