using ServerLog.Application.ViewModels;
using ServerLog.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Commons.Message.MqEvents;

namespace ServerLog.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<MoneyChangedMqEvent, MoneyLogInfo>().ReverseMap();
        }
    }
}
