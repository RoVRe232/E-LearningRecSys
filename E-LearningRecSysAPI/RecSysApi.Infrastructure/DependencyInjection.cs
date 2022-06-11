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
            var connection = @"Server=DESKTOP-OMFT137\SQLEXPRESS;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = "Server=host.docker.internal,5010;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";

            //Swap to this connection string for Update Database commands
            //var connection = "Server=127.0.0.1,5010;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";
            services.AddTransient<IVideosStorageService, VideosStorageService>();
            services.AddSingleton<IHttpService, HttpService>();
            services.AddDbContext<RecSysApiContext>(options => options.UseSqlServer(connection, b => b.MigrationsAssembly("RecSysApi.Infrastructure")));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISessionService, SessionService>();

            return services;
        }
    }
}
