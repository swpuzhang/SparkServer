using Friend.Application.ViewModels;
using Friend.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Friend.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FriendVM, FriendInfo>().ReverseMap();
        }
    }
}
