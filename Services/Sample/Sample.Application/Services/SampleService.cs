﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sample.Application.ViewModels;
using Sample.Domain;
using Sample.Domain.Commands;
using Sample.Domain.Models;
using Sample.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Sample.Application.Services
{
    public class SampleService : ISampleService
    {
        private readonly ISampleInfoRepository _repository;
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public SampleService(ISampleInfoRepository repository, IMapper mapper, IMediatorHandler bus)
        {
            _repository = repository;
            _mapper = mapper;
            _bus = bus;
        }
        public SampleVM GetById(Int64 id)
        {
            return _mapper.Map<SampleVM>( _repository.GetById(id));
        }


        public async Task<BodyResponse<SampleInfo>> Login(SampleVM sampleVM)
        {
            return await _bus.SendCommand(new SampleCommand(_mapper.Map<SampleInfo>(sampleVM)));
        }
    }
}
