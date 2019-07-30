using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Money.Application.ViewModels;
using Money.Domain;
using Money.Domain.Commands;
using Money.Domain.Models;
using Money.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Money.Application.Services
{
    public class MoneyAppService : IMoneyAppService
    {
        private readonly IMoneyInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public MoneyAppService(IMoneyInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }
        public MoneyVM GetById(Int64 id)
        {
            return _mapper.Map<MoneyVM>( _repository.GetById(id));
        }

        

        public async Task<HasBodyResponse<MoneyResponse>> Login(MoneyVM moneyVM)
        {
            return await _bus.SendCommand(new LoginCommand(_mapper.Map<MoneyInfo>(moneyVM)));
        }
    }
}
