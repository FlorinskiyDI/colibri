using AutoMapper;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.Common.Context;
using dataaccesscore.Abstractions.Uow;

namespace Survey.ApplicationLayer.Services
{
    public class SurveySectionService : ISurveySectionService
    {
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;
        private readonly IUserService _userService;

        public SurveySectionService(
            IUowProvider uowProvider,
            IMapper mapper,
            IUserService userService
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
            this._userService = userService;
        }

        public async Task<IEnumerable<SurveySectionDto>> GetAll()
        {
            Guid userId = Guid.Parse(NTContext.Context.UserId);
            IEnumerable<SurveySections> items;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                items = await repositorySurveySection.GetAllAsync();
                await uow.SaveChangesAsync();
            }
            return Mapper.Map<IEnumerable<SurveySections>, IEnumerable<SurveySectionDto>>(items);
        }


        public async Task<IEnumerable<SurveyExtendViewModel>> GetSurveysWithRespondentCount()
        {
            Guid userId = Guid.Parse(NTContext.Context.UserId);
            IEnumerable<SurveySections> items;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                items = await repositorySurveySection.GetAllAsync();
                await uow.SaveChangesAsync();
            }
            return Mapper.Map<IEnumerable<SurveySections>, IEnumerable<SurveyExtendViewModel>>(items);
        }


        public async Task<IEnumerable<SurveySectionDto>> GetUnlockedSuerveys()
        {
            //Guid userId = Guid.Parse(NTContext.Context.UserId);
            IEnumerable<SurveySections> items;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                items = await repositorySurveySection.QueryAsync(x => x.IsLocked == false);
                await uow.SaveChangesAsync();
            }
            return Mapper.Map<IEnumerable<SurveySections>, IEnumerable<SurveySectionDto>>(items);
        }


        public async Task<SurveyModel> GetAsync(Guid surveyId)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                SurveySections survey = new SurveySections();
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                survey = await repositorySurveySection.GetAsync(surveyId);
                await uow.SaveChangesAsync();
                SurveyModel surveyModel = Mapper.Map<SurveySections, SurveyModel>(survey);
                return surveyModel;
            }
        }


        public async Task<bool> SetLockState(Guid id, bool state)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                SurveySections survey = new SurveySections();
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                survey = await repositorySurveySection.GetAsync(id);
                survey.IsLocked = state;
                survey = repositorySurveySection.Update(survey);
                await uow.SaveChangesAsync();
                return survey.IsLocked == state;
            }
        }


        public async Task<Guid> Update(SurveyModel survey)
        {
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                var surveyModel = await repositorySurveySection.GetAsync(Guid.Parse(survey.Id));
                surveyModel.Name = survey.Name;
                surveyModel.Description = survey.Description;
                surveyModel.IsOpenAccess = survey.IsOpenAccess;
                surveyModel.IsLocked = survey.IsLocked;
                surveyModel.IsShowDescription = survey.IsShowDescription;
                surveyModel.IsShowProcessCompletedText = survey.IsShowProcessCompletedText;
                surveyModel.ProcessCompletedText = survey.ProcessCompletedText;
                repositorySurveySection.Update(surveyModel);

                await uow.SaveChangesAsync();
                return surveyModel.Id;
            }
        }


        public async Task<Guid> AddAsync(SurveyModel survey)
        {
            Guid userId;
            UsersDto existUser = await _userService.GetAsync(Guid.Parse(NTContext.Context.UserId));
            if (existUser == null)
            {
                userId = await _userService.AddAsync(Guid.Parse(NTContext.Context.UserId));
            }
            else
            {
                userId = existUser.Id;
            }
            SurveySectionDto surveyDto = new SurveySectionDto()
            {
                Description = survey.Description,
                Name = survey.Name,
                DateCreated = System.DateTime.Now,
                IsShowDescription = survey.IsShowDescription,
                IsShowProcessCompletedText = survey.IsShowProcessCompletedText,
                ProcessCompletedText = survey.ProcessCompletedText,
                UserId = userId,
                IsOpenAccess = survey.IsOpenAccess,
                IsLocked = survey.IsLocked
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                SurveySections surveyEntity = Mapper.Map<SurveySectionDto, SurveySections>(surveyDto);
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                await repositorySurveySection.AddAsync(surveyEntity);
                await uow.SaveChangesAsync();
                return surveyEntity.Id;
            }
        }
    }
}
