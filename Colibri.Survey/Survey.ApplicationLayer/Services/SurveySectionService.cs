using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Survey.ApplicationLayer.Dtos.Models;

namespace Survey.ApplicationLayer.Services
{
    public class SurveySectionService : ISurveySectionService
    {
        protected readonly IUowProvider UowProvider;
        protected readonly IMapper Mapper;

        public SurveySectionService(
            IUowProvider uowProvider,
            IMapper mapper
        )
        {
            this.UowProvider = uowProvider;
            this.Mapper = mapper;
        }

        public IEnumerable<SurveySectionDto> GetAll()
        {
            IEnumerable<SurveySections> items;
            using (var uow = UowProvider.CreateUnitOfWork())
            {
                var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                items = repositorySurveySection.GetAll();
            }
            return Mapper.Map<IEnumerable<SurveySections>, IEnumerable<SurveySectionDto>>(items);
        }

        public async Task<Guid> AddAsync(SurveyModel survey)
        {

            SurveySectionDto surveyDto= new SurveySectionDto()
            {
                //Id = new Guid(),
                Description = survey.Description,
                Name = survey.Name
            };

            using (var uow = UowProvider.CreateUnitOfWork())
            {
                try
                {
                    SurveySections surveyEntity = Mapper.Map<SurveySectionDto, SurveySections>(surveyDto);
                    var repositorySurveySection = uow.GetRepository<SurveySections, Guid>();
                    await repositorySurveySection.AddAsync(surveyEntity);
                    await uow.SaveChangesAsync();

                    return surveyEntity.Id;
                }
                catch (Exception e)
                {
                    Console.Write(e);
                    throw;
                }
            }
            
        }
    }
}
