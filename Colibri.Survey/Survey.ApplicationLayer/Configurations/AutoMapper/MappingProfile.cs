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
            CreateMap<PagesDto, Pages>();
            CreateMap<QuestionsDto, Questions>();
            CreateMap<InputTypesDto, InputTypes>();
            CreateMap<OptionGroupsDto, OptionGroups>();
            CreateMap<OptionChoisesDto, OptionChoises>();
        }
        private void DtoToEntity()
        {
            CreateMap<SurveySections, SurveySectionDto>();
            CreateMap<Pages, PagesDto>();
            CreateMap<Questions, QuestionsDto> ();
            CreateMap<InputTypes, InputTypesDto>();
            CreateMap<OptionGroups, OptionGroupsDto>();
            CreateMap<OptionChoises, OptionChoisesDto>();
            // models
            // entities
            //CreateMap<PersonalClaimDto, PersonalClaim>();
        }
    }
}
