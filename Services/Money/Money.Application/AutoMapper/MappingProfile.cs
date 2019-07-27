using Sample.Application.ViewModels;
using Sample.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SampleVM, SampleInfo>().ReverseMap();
        }
    }
}
