using Account.Application.ViewModels;
using Account.Domain.Models;
using AutoMapper;
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
        }
    }
}
