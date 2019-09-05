using AppSetting.Domain.Commands;
using AppSetting.Domain.Models;
using AppSetting.Domain.RepositoryInterface;
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

namespace AppSetting.Domain.CommandHandlers
{
    public class AppSettingCommandHandler :
        IRequestHandler<FacebackCommand, BodyResponse<string>>,
        IRequestHandler<GetUnreadReplyCommand, BodyResponse<FadebackReplyNum>>
    {
        //private readonly readonly IRequestClient<DoSomething> _requestClient;
    
        protected readonly IMediatorHandler _bus;
        private readonly IFadebackInfoRepository _appsettingRepository;
        private readonly IAppSettingRedisRepository _redis;
        public AppSettingCommandHandler(IFadebackInfoRepository rep, IAppSettingRedisRepository redis, IMediatorHandler bus)
        {
            _appsettingRepository = rep;
            _redis = redis;
            _bus = bus;
        }
        public Task<BodyResponse<string>> Handle(FacebackCommand request, CancellationToken cancellationToken)
        {
            //插入一条到数据库
            BodyResponse<string> response = new BodyResponse<string>(StatusCodeDefines.Success, null, null);
            return Task.FromResult(response);

        }

        public Task<BodyResponse<FadebackReplyNum>> Handle(GetUnreadReplyCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
