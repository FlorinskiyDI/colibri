using AutoMapper;
using Survey.ApplicationLayer.Dtos.Entities;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Models.IdentityServer.Pager;
using Survey.ApplicationLayer.Dtos.Models.Questions;
using Survey.DomainModelLayer.Entities;
using Survey.DomainModelLayer.Models;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.IdentityServer.Pager;
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
            ViewModelToEntity();
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
            CreateMap<UsersDto, Users>();
        }
        private void EntityToDto()
        {
            // models
            CreateMap<PageSearchEntryDto, PageSearchEntryModel>();
            CreateMap(typeof(PageDataDto<>), typeof(PageDataModel<>));
            CreateMap<IdentityUserDto, IdentityUserModel>();
            CreateMap<IdentityUserDto, IdentityUserModel>();

            // entities
            CreateMap<SurveySections, SurveySectionDto>();
            CreateMap<Pages, PagesDto>();
            CreateMap<Questions, QuestionsDto>();
            CreateMap<InputTypes, InputTypesDto>();
            CreateMap<OptionGroups, OptionGroupsDto>();
            CreateMap<OptionChoises, OptionChoisesDto>();
            CreateMap<Users, UsersDto>();
            //CreateMap<PersonalClaimDto, PersonalClaim>();
        }

        private void ViewModelToEntity()
        {
            // models
            CreateMap<BaseQuestionModel, Questions>()
                .ForMember(dest => dest.OrderNo, opt => opt.MapFrom(src => src.Order));

        }
    }
}
