﻿using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameSample.Application.AutoMapper
{
    public class AutoMapperConfig
    {
        public static MapperConfiguration RegisterMappings()
        {

            return new MapperConfiguration(cfg =>
            {

                cfg.AddProfile(new MappingProfile());

            });
        }
    }
}
