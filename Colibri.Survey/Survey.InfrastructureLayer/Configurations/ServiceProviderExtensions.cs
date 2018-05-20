using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using storagecore.EntityFrameworkCore;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.InfrastructureLayer.Context;
using Survey.InfrastructureLayer.IdentityServices;
using Survey.InfrastructureLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.InfrastructureLayer.Configurations
{
    public static class ServiceProviderExtensions
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services, string connectionString)
        {

            AddDataAccess(services, connectionString);
            AddServices(services);
            AddRepositories(services);
            return services;
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IGroupRequestService, GroupRequestService>();
            services.AddScoped<IUserRequestService, UserRequestService>();

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ISurveySectionRepository, SurveySectionRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IInputTypeRepository, InputTypeRepository>();
            services.AddScoped<IOptionGroupRepository, OptionGroupRepository>();
            services.AddScoped<IOptionChoiceRepository, OptionChoiceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }


        private static void AddDataAccess(IServiceCollection services, string connectionString)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Survey.InfrastructureLayer")));
            services.AddStorageCoreDataAccess<ApplicationDbContext>();
        }
    }
}
