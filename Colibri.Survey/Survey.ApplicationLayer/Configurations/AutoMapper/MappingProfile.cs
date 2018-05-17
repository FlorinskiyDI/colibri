using AutoMapper;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.DomainModelLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Survey.ApplicationLayer.Configurations.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            EntityToDto();
            DtoToEntity();
        }


        private void EntityToDto()
        {
            // models
            // entities
            CreateMap<SurveySectionDto, SurveySections>();
        }
        private void DtoToEntity()
        {
            CreateMap<SurveySections, SurveySectionDto>();
            // models
            // entities
            //CreateMap<PersonalClaimDto, PersonalClaim>();
        }
    }
}
