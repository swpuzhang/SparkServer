using Account.Domain.Commands;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
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
using Commons.MqEvents;
using AutoMapper;
using Serilog;
using Account.Domain.Events;
using Commons.MqCommands;
using Account.Domain.Manager;

namespace Account.Domain.CommandHandlers
{
    public class GetLevelInfoCommandHandler :
        IRequestHandler<GetLevelInfoCommand, BodyResponse<LevelInfo>>
        
    {
        private readonly ILevelInfoRepository _levelRepository;
        private readonly IAccountRedisRepository _redis;
        private readonly LevelManager _levelManager;
        public GetLevelInfoCommandHandler(ILevelInfoRepository rep,
            IAccountRedisRepository redis,
            LevelManager levelManager)
        {
            _levelRepository = rep;
            _redis = redis;
            _levelManager = levelManager;
        }


        public async Task<BodyResponse<LevelInfo>> Handle(GetLevelInfoCommand request, 
            CancellationToken cancellationToken)
        {
            //读取redis account信息
            LevelInfo levelinfo = await _redis.GetLevelInfo(request.Id);
            if (levelinfo == null)
            {
                //从数据库中获取
                using (var loker = _redis.Loker(KeyGenHelper
                .GenUserKey(request.Id, LevelInfo.ClassName)))
                {
                    loker.Lock();
                    levelinfo = await _levelRepository.FindAndAdd(request.Id, 
                        new LevelInfo(request.Id, 1, 0, _levelManager.GetNeedExp(1)));
                    _ = _redis.SetLevelInfo(levelinfo);
                }
                
            }
            
            BodyResponse<LevelInfo> response = new BodyResponse<LevelInfo>(StatuCodeDefines.Success,
                null, levelinfo);
            
            return response;

        }

        
    }
}
