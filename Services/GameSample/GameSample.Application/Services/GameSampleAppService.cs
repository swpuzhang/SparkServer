using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GameSample.Application.ViewModels;
using GameSample.Domain;
using GameSample.Domain.Commands;
using GameSample.Domain.Models;
using GameSample.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace GameSample.Application.Services
{
    public class GameSampleAppService : IGameSampleAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public GameSampleAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }



    }
}
