using Account.Application.ViewModels;
using Account.Domain.Models;
using AutoMapper;
using Commons.Extenssions;
using Commons.MqEvents;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AccountVM, AccountInfo>().ReverseMap();
            CreateMap<AccountResponse, LoginMqEvent >()
                .AfterMap((src, dest) =>
                {
                    dest.ActionTime = DateTimeHelper.NowTimeStamp();
                    dest.AggregateId = Guid.NewGuid();
                });
            CreateMap<AccountResponse, RegistMqEvent>()
                .AfterMap((src, dest) => {
                    dest.ActionTime = DateTimeHelper.NowTimeStamp();
                    dest.AggregateId = Guid.NewGuid();
                });
        }
    }
}
