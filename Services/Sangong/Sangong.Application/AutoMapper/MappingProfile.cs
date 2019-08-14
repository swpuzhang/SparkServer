using Sangong.Application.ViewModels;
using Sangong.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Sangong.MqCommands;
using Sangong.GameMessage;
using Sangong.Domain.Logic;

namespace Sangong.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SangongVM, SangongInfo>().ReverseMap();
            CreateMap<SangongMatchingResponseInfo, MatchingResponseVM>().ReverseMap();
           
          
        }
    }
}
