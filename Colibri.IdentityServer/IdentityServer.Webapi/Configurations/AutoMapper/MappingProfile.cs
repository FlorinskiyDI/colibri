using AutoMapper;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations.AutoMapper
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
            CreateMap<GroupDto, Groups>();
        }
        private void EntityToDto()
        {
            CreateMap<Groups, GroupDto>();
        }

        private void ViewModelToEntity()
        {
           

        }
    }
}
