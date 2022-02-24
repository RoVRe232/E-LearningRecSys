using AutoMapper;
using RecSysApi.Application.Dtos;
using RecSysApi.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application.Commons.Helpers
{
    public class CustomMapper<TSource, TDestination> : ICustomMapper<TSource, TDestination>
    {
        private MapperConfiguration _mapperConfiguration;
        private Mapper _mapper;
        public CustomMapper()
        {
            _mapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<TSource, TDestination>();
                //.ForMember(dest => dest.BulkKeywords, act => act.MapFrom(src => src.BulkKeywords)); DO this in case dto name does not match dest obj name
            });

            _mapper = new Mapper(_mapperConfiguration);
        }

        public TDestination Map(TSource sourceDto)
        {
            throw new NotImplementedException();
        }
    }
}
