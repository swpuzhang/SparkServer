using MsgCenter.Application.ViewModels;
using MsgCenter.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MsgCenter.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<MsgCenterVM, MsgCenterInfo>().ReverseMap();
        }
    }
}
