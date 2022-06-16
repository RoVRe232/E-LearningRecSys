using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Application.Interfaces;
using RecSysApi.Domain.Interfaces;
using RecSysApi.Infrastructure.Context;
using RecSysApi.Infrastructure.Services;
using RecSysApi.Infrastructure.UnitsOfWork;

namespace RecSysApi.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayerDependencies(this IServiceCollection services)
        {
            //var connection = @"Server=DESKTOP-3LKRNT7\SQLEXPRESS;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";
            var connection = @"Server=DESKTOP-OMFT137\SQLEXPRESS;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = "Server=host.docker.internal,5010;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";

            //Swap to this connection string for Update Database commands
            //var connection = "Server=127.0.0.1,5010;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";
            services.AddSingleton<IHttpService, HttpService>();
            services.AddTransient<IVideosStorageService, VideosStorageService>();
            services.AddDbContext<RecSysApiContext>(options =>
            {
                options.UseSqlServer(connection, b => b.MigrationsAssembly("RecSysApi.Infrastructure"));
                options.EnableSensitiveDataLogging(true);
            })
            ;
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<ISessionService, SessionService>();

            return services;
        }
    }
}
