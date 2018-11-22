using dataaccesscore.EFCore;
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
                .AddDataAccess(connectionString);
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddTransient<IAppUserService, AppUserService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupMemberService, GroupMemberService>();
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
            services.AddScoped<IAppUserGroupRepository, AppUserGroupRepository>();
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

        //public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        //{
        //    services.AddDbContext<ApplicationDbContext>(options =>
        //        options.UseSqlServer(connectionString));
        //    services.AddStorageCoreDataAccess<ApplicationDbContext>();
        //    return services;
        //}

    }
}
