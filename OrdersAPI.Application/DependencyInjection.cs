using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using OrdersAPI.Application.Dtos;
using OrdersAPI.Application.Dtos.Account;
using OrdersAPI.Application.Dtos.Courses;
using OrdersAPI.Application.Dtos.Orders;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Orders;
using RecSysApi.Domain.Entities.Products;
using System;
using System.Reflection;

namespace OrdersAPI.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Account, AccountDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<VideoSource, VideoSourceDTO>()
                    .IgnoreAllNonExisting()
                    .PreserveReferences()
                    .ReverseMap();
                cfg.CreateMap<Video, VideoDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Price, PriceDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Section, SectionDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Publisher, PublisherDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Course, CourseDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Order, OrderDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
            });
            autoMapperConfig.AssertConfigurationIsValid();

            services.AddSingleton(m => autoMapperConfig.CreateMapper());

            return services;
        }

        private static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>
            (this IMappingExpression<TSource, TDestination> expression)
        {
            var flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }
            return expression;
        }
    }
}
