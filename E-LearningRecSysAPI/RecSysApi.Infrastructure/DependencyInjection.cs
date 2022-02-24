using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
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
        public static IServiceCollection AddInfrastructureLayerDependencies(this IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";

            services.AddSingleton<IHttpService, HttpService>();
            services.AddScoped<IVideosRetrievalService, VideosRetrievalService>();
            services.AddDbContext<RecSysApiContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("RecSysApi.Infrastructure")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
