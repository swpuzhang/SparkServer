using Sangong.Application.ViewModels;
using Sangong.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sangong.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<SangongVM, SangongInfo>().ReverseMap();
        }
    }
}
