using Dummy.Application.ViewModels;
using Dummy.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Dummy.MqCommands;
using Dummy.GameMessage;
using Dummy.Domain.Logic;

namespace Dummy.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<DummyVM, DummyInfo>().ReverseMap();
            CreateMap<DummyMatchingResponseInfo, MatchingResponseVM>().ReverseMap();
           
          
        }
    }
}
