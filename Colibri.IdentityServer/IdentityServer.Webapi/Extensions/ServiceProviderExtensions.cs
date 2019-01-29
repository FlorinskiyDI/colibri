using AutoMapper;
using dataaccesscore.EFCore;
using IdentityServer.Webapi.Configurations.AutoMapper;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Infrastructure.Messaging;
using IdentityServer.Webapi.Infrastructure.Razor;
using IdentityServer.Webapi.Repositories;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityServer.Webapi.Extensions
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            return services
                .AddRepositories()
                .AddServices()
                .AddDataAccess(connectionString)
                .AddAutoMapper();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupNodeService, GroupNodeService>();
            services.AddTransient<IMemberService, MemberService>();
            services.AddTransient<IEmailSenderService, EmailSenderService>();

            
            //
            services.AddTransient<IViewRenderResolver, ViewRenderResolver>();
            services.AddTransient<IEmailSender, EmailSender>();
            //
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IMemberGroupRepository, MemberGroupRepository>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            //
            return services;
        }

        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //     options.UseSqlServer(connectionString));
            //services.AddStorageCoreDataAccess<ApplicationDbContext>();
            //
            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MappingProfile()); });
            services.AddSingleton<IMapper>(sp => config.CreateMapper());
            return services;
        }
        //public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));
        //    services.AddStorageCoreDataAccess<ApplicationDbContext>();
        //    return services;
        //}

    }
}
