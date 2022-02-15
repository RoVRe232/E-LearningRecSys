using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Repositories;
using RecSysApi.Infrastructure.Services;
using RecSysApi.Infrastructure.UnitsOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrsatructureLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IQueryRepository, QueryRepository>();
            services.AddScoped<IVideoRepository, VideoRepository>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IVideosRetrievalService, VideosRetrievalService>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
