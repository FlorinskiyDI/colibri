using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.InfrastructureLayer.IdentityServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.InfrastructureLayer.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            return services
                .AddServices()
                .AddRepositories();
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IGroupRequestService, GroupRequestService>();
            services.AddScoped<IUserRequestService, UserRequestService>();
            return services;
        }
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services;
        }
       
    }
}
