using AutoMapper;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Models;
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


        private void DtoToEntity()
        {
            // models dto
            CreateMap<Groups, GroupDto>();
            CreateMap<IdentityUserModel, IdentityUserDto>();
            // entities dto
            CreateMap<SurveySectionDto, SurveySections>();
            CreateMap<PagesDto, Pages>();
            CreateMap<QuestionsDto, Questions>();
            CreateMap<InputTypesDto, InputTypes>();
            CreateMap<OptionGroupsDto, OptionGroups>();
            CreateMap<OptionChoisesDto, OptionChoises>();
        }
        private void EntityToDto()
        {
            // models
            CreateMap<GroupDto, Groups>();
            CreateMap<IdentityUserDto, IdentityUserModel>();
            // entities
            CreateMap<SurveySections, SurveySectionDto>();
            CreateMap<Pages, PagesDto>();
            CreateMap<Questions, QuestionsDto>();
            CreateMap<InputTypes, InputTypesDto>();
            CreateMap<OptionGroups, OptionGroupsDto>();
            CreateMap<OptionChoises, OptionChoisesDto>();
            //CreateMap<PersonalClaimDto, PersonalClaim>();
        }
    }
}
