using GameSample.Application.ViewModels;
using GameSample.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using GameSample.MqCommands;
using GameSample.GameMessage;
using GameSample.Domain.Logic;

namespace GameSample.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GameSampleVM, GameSampleInfo>().ReverseMap();
            CreateMap<GameSampleMatchingResponseInfo, MatchingResponseVM>().ReverseMap();
           
          
        }
    }
}
