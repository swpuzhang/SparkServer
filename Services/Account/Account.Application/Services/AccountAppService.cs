using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Account.Application.ViewModels;
using Account.Domain;
using Account.Domain.Commands;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Account.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public AccountAppService(IAccountInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }
        public AccountVM GetById(Int64 id)
        {
            return _mapper.Map<AccountVM>( _repository.GetById(id));
        }

        public void Update(AccountVM accountViewModel)
        {
            _repository.Update(_mapper.Map<AccountInfo>(accountViewModel));
        }

        public async Task<HasBodyResponse<AccountResponse>> Login(AccountVM accountViewModel)
        {
            return await _bus.SendCommand(new LoginCommand(_mapper.Map<AccountInfo>(accountViewModel)));
        }
    }
}
