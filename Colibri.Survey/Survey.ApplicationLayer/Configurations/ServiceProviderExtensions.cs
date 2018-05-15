using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Survey.ApplicationLayer.Configurations.AutoMapper;
using Survey.ApplicationLayer.Services;
using Survey.ApplicationLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddApplicationDependencies(this IServiceCollection services)
        {
            return services
                .AddAutoMapper()
                .AddServices();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {            
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            services.AddSingleton<IMapper>(sp => config.CreateMapper());
            return services;
        }
    }
}
