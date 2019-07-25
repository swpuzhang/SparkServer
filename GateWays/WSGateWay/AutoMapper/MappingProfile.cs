using AutoMapper;
using Commons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace WSGateWay.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ServerRequest, ToAppRequest>().ReverseMap();
        }
    }
}
