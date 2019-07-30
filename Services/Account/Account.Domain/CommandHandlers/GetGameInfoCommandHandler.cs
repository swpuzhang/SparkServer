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
    public class GetGameInfoCommandHandler :
        IRequestHandler<GetGameInfoCommand, HasBodyResponse<GameInfo>>
        
    {
        private readonly IGameInfoRepository _gameRepository;
        private readonly IAccountRedisRepository _redis;

        public GetGameInfoCommandHandler(IGameInfoRepository rep,
            IAccountRedisRepository redis)
        {
            _gameRepository = rep;
            _redis = redis;
        }


        public async Task<HasBodyResponse<GameInfo>> Handle(GetLevelInfoCommand request, 
            CancellationToken cancellationToken)
        {
            //读取redis account信息
            GameInfo gameinfo = await _redis.GetGameInfo(request.Id);
            if (gameinfo == null)
            {
                using (var loker = _redis.Loker(KeyGenHelper
                .GenUserKey(request.Id, GameInfo.ClassName)))
                {
                    loker.Lock();
                    gameinfo = await _gameRepository.FindAndAdd(request.Id, new GameInfo(request.Id, 0, 0, 0));
                    _ = _redis.SetGameInfo(gameinfo);
                }
            }
            
            HasBodyResponse<GameInfo> response = new HasBodyResponse<GameInfo>(StatuCodeDefines.Success,
                null, gameinfo);
            
            return response;
        }

        public Task<HasBodyResponse<GameInfo>> Handle(GetGameInfoCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
