using AutoMapper;
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
        }
        private void DtoToEntity()
        {
            // models
            // entities
            //CreateMap<PersonalClaimDto, PersonalClaim>();
        }
    }
}
