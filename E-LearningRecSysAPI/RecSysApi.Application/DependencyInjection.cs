using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Application.Dtos.Account;
using RecSysApi.Application.Dtos.Courses;
using RecSysApi.Application.Dtos.Video;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Services;
using RecSysApi.Domain.Entities;
using RecSysApi.Domain.Entities.Account;
using RecSysApi.Domain.Entities.Products;
using System.Reflection;

namespace RecSysApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services)
        {
            var autoMapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<VideoSource, VideoSourceDTO>()
                    .IgnoreAllNonExisting()
                    .PreserveReferences()
                    .ReverseMap();
                cfg.CreateMap<Video, VideoDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Price, PriceDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Section, SectionDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Publisher, PublisherDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
                cfg.CreateMap<Course, CourseDTO>().IgnoreAllNonExisting().PreserveReferences().ReverseMap();
            });
            autoMapperConfig.AssertConfigurationIsValid();

            services.AddSingleton(m => autoMapperConfig.CreateMapper());
            services.AddTransient<IVideosService, VideosService>();
            services.AddTransient<ICoursesService, CoursesService>();
            services.AddTransient<IVideosService, VideosService>();
            services.AddTransient<IAuthService, AuthService>();

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
