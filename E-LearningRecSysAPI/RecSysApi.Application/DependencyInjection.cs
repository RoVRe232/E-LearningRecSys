using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Application.Interfaces;
using RecSysApi.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationLayerDependencies(this IServiceCollection services)
        {
            services.AddScoped<IVideosLookupService, VideosLookupService>();
            services.AddScoped<IVideosJsonService, VideosJsonService>();

            return services;
        }
    }
}
