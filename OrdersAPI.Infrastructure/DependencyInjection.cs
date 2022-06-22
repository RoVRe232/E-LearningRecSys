using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrdersAPI.Application.Interfaces;
using OrdersAPI.Infrastructure.Context;
using OrdersAPI.Infrastructure.Services;
using OrdersAPI.Infrastructure.UnitsOfWork;
using System;

namespace OrdersAPI.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayerDependencies(this IServiceCollection services)
        {
            var connection = @"Server=DESKTOP-OMFT137\SQLEXPRESS;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0";
            //var connection = "Server=host.docker.internal,6003;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";

            //Swap to this connection string for Update Database commands
            //var connection = "Server=127.0.0.1,5010;User ID=SA;Password=abcDEF123#;Database=RecSysApiDb;Trusted_Connection=True;ConnectRetryCount=0;Integrated Security=false";
            services.AddSingleton<IHttpService, HttpService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddDbContext<RecSysApiContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
