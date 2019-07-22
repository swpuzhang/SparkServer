using System;
using System.Collections.Generic;
using System.Text;
using Account.Application.ViewModels;
using Account.Domain;
using Account.Domain.Models;
using AutoMapper;
using Commons.Infrastruct;

namespace Account.Application.Services
{
    public class AccountAppService : IAccountAppService
    {
        private readonly IAccountInfoRepository _repository;
        private readonly IMapper _mapper;
        public AccountAppService(IAccountInfoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public AccountVM GetById(Int64 id)
        {
            return _mapper.Map<AccountVM>( _repository.GetById(id));
        }

        public HasBodyResponseVM<AccounResponsetVM> Login(AccountVM StudentViewModel)
        {
            return new HasBodyResponseVM<AccounResponsetVM>();
        }

        public void Update(AccountVM StudentViewModel)
        {
            _repository.Update(_mapper.Map<AccountInfo>(StudentViewModel));
        }
    }
}
