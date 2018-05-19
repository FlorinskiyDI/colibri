using Microsoft.Extensions.DependencyInjection;
using Survey.Webapi.Services.TypeHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddWebApiDependencies(this IServiceCollection services)
        {
            return services
                .AddServices();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITypeHelperService, TypeHelperService>();
           
            return services;
        }
    }
}
