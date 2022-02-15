using Microsoft.Extensions.DependencyInjection;
using RecSysApi.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecSysApi.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainLayerDependencies(this IServiceCollection services)
        {

            return services;
        }
    }
}
