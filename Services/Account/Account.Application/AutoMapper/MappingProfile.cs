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
            CreateMap<AccountResponse, AccountResponseVM>()
                .ForMember(x=>x.CurCoins, (map)=>map.MapFrom(dto => dto.MoneyInfo.CurCoins))
                .ForMember(x => x.CurDiamonds, (map) => map.MapFrom(dto => dto.MoneyInfo.CurDiamonds))
                .ReverseMap();
            CreateMap<AccountDetail, AccountDetailVM>().ReverseMap();
            CreateMap<GameInfoVM, GameInfo>().ReverseMap();
            CreateMap<LevelInfoVM, LevelInfo>().ReverseMap();
            CreateMap<MoneyInfoVM, MoneyInfo>().ReverseMap();
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
