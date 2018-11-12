using dataaccesscore.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Survey.DomainModelLayer.Contracts.Repositories;
using Survey.InfrastructureLayer.Context;
using Survey.InfrastructureLayer.IdentityServerServices;
using Survey.InfrastructureLayer.IdentityServerServices.Interfaces;
using Survey.InfrastructureLayer.IdentityServices;
using Survey.InfrastructureLayer.Repositories;

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
            services.AddScoped<IIdentityUserRequestService, IdentityUserRequestService>();

        }
        private static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<ISurveySectionRepository, SurveySectionRepository>();
            services.AddScoped<IPageRepository, PageRepository>();
            services.AddScoped<IRespondentRepository, RespondentRepository>();
            services.AddScoped<IAnswerRepository, AnswerRepository>();
            services.AddScoped<IQuestionOptionRepository, QuestionOptionRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IInputTypeRepository, InputTypeRepository>();
            services.AddScoped<IOptionGroupRepository, OptionGroupRepository>();
            services.AddScoped<IOptionChoiceRepository, OptionChoiceRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
        }


        private static void AddDataAccess(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Survey.InfrastructureLayer")));
            services.AddStorageCoreDataAccess<ApplicationDbContext>();
        }
    }
}
