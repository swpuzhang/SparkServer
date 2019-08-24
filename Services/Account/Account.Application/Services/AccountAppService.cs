using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Account.Application.ViewModels;
using Account.Domain;
using Account.Domain.Commands;
using Account.Domain.Events;
using Account.Domain.Models;
using Account.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Extenssions.Defines;
using Commons.Infrastruct;
using Commons.MqCommands;

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

        public async Task<BodyResponse<AccountResponseVM>> Login(AccountVM accountViewModel)
        {
            var response = await _bus.SendCommand(new LoginCommand(_mapper.Map<AccountInfo>(accountViewModel)));
            AccountResponseVM responseVM = _mapper.Map<AccountResponseVM>(response.Body);
            return new BodyResponse<AccountResponseVM>(response.StatusCode, response.ErrorInfos, responseVM);

        }

        public async Task<BodyResponse<AccountDetailVM>> GetSelfAccount(long id)
        {
            var response = await _bus.SendCommand(new GetSelfAccountCommand(id));
            AccountDetailVM info = _mapper.Map<AccountDetailVM>(response.Body);
            return new BodyResponse<AccountDetailVM>(response.StatusCode, response.ErrorInfos, info);
        }

        public async Task<BodyResponse<GetAccountBaseInfoMqResponse>> GetAccountBaseInfo(long id)
        {
            var response = await _bus.SendCommand(new GetAccountBaseInfoCommand(id));
            if (response.StatusCode != StatusCodeDefines.Success)
            {
                return new BodyResponse<GetAccountBaseInfoMqResponse>(response.StatusCode, response.ErrorInfos);
            }
            return new BodyResponse<GetAccountBaseInfoMqResponse>(StatusCodeDefines.Success, null,
                _mapper.Map<GetAccountBaseInfoMqResponse>(response.Body));
        }

        public void FinishRegisterReward(long id)
        {
             _bus.RaiseEvent(new FinishRegisterRewardEvent(id));
        }

        public Task<BodyResponse<GetIdByPlatformMqResponse>> GetIdByPlatform(string platformAccount, int type)
        {
            return _bus.SendCommand(new GetIdByPlatformCommand(platformAccount, type));
        }

    }
}
