﻿using Money.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace Money.WebApi.Extenssions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            //var cfg = AutoMapperConfig.RegisterMappings();
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddSingleton(cfg);
            //services.AddAutoMapperSetup()
        }
    }
}
