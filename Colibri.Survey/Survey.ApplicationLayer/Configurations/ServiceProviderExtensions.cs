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
            services.AddScoped<ISurveySectionService, SurveySectionService>();
            services.AddScoped<IRespondentService, RespondentService>();
            services.AddScoped<ISurveySectionRespondentService, SurveySectionRespondentServie>();
            services.AddScoped<IPageService, PageService>();
            services.AddScoped<IQuestionOptionService, QuestionOptionService>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<IAnswerService, AnswerService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<IInputTypeService, InputTypeService>();
            services.AddScoped<IOptionGroupService, OptionGroupService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IOptionChoiceService, OptionChoiceService>();
            services.AddScoped<IExcelService, ExcelService>();
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
