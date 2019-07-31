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
using Commons.Extenssions.Defines;
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

        public async Task<HasBodyResponse<AccountResponseVM>> Login(AccountVM accountViewModel)
        {
            var response = await _bus.SendCommand(new LoginCommand(_mapper.Map<AccountInfo>(accountViewModel)));
            AccountResponseVM responseVM = _mapper.Map<AccountResponseVM>(response.Body);
            return new HasBodyResponse<AccountResponseVM>(response.StatusCode, response.ErrorInfos, responseVM);

        }

        public async Task<HasBodyResponse<AccountDetailVM>> GetSelfAccount(long id)
        {
            var response = await _bus.SendCommand(new GetSelfAccountCommand(id));
            AccountDetailVM info = _mapper.Map<AccountDetailVM>(response.Body);
            return new HasBodyResponse<AccountDetailVM>(response.StatusCode, response.ErrorInfos, info);
        }
    }
}
