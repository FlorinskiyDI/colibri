using AutoMapper;
using storagecore.Abstractions.Uow;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
