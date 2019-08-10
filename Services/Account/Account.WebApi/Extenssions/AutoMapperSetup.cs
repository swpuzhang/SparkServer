using Account.Application.AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson.Serialization;
using Account.Domain.Models;

namespace Account.WebApi.Extenssions
{
    public static class AutoMapperSetup
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            //var cfg = AutoMapperConfig.RegisterMappings();
            services.AddAutoMapper(typeof(MappingProfile));
            //services.AddSingleton(cfg);
            //services.AddAutoMapperSetup()
            BsonClassMap.RegisterClassMap<AccountInfo>();
        }
    }
}
