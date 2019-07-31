using Money.Application.ViewModels;
using Money.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.MqCommands;

namespace Money.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            
            CreateMap<GetMoneyMqResponse, MoneyInfo>().ReverseMap();
        }
    }
}
