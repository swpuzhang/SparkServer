using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AppSetting.Application.ViewModels;
using AppSetting.Domain;
using AppSetting.Domain.Commands;
using AppSetting.Domain.Models;
using AppSetting.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace AppSetting.Application.Services
{
    public class AppSettingService : IAppSettingService
    {
        private readonly IFadebackInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public AppSettingService(IFadebackInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }
     
    }
}
