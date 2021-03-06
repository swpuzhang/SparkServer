﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Sangong.Application.ViewModels;
using Sangong.Domain;
using Sangong.Domain.Commands;
using Sangong.Domain.Models;
using Sangong.Domain.RepositoryInterface;
using AutoMapper;
using Commons.Domain.Bus;
using Commons.Domain.Models;
using Commons.Infrastruct;

namespace Sangong.Application.Services
{
    public class SangongAppService : ISangongAppService
    {
        private readonly IMapper _mapper;
        private readonly IMediatorHandler _bus;
        public SangongAppService(IMapper mapper, IMediatorHandler bus)
        {
            _mapper = mapper;
            _bus = bus;
        }



    }
}
