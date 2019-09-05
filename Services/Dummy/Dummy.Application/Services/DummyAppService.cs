using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dummy.Application.ViewModels;
using Dummy.Domain;
using Dummy.Domain.Commands;
using Dummy.Domain.Models;
using Dummy.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Dummy.Application.Services
{
    public class DummyAppService : IDummyAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public DummyAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }



    }
}
