using AutoMapper;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Models;
using Survey.InfrastructureLayer.IdentityServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRequestService _groupRequestService;
        protected readonly IMapper Mapper;

        public GroupService(
            IGroupRequestService groupRequestService,
            IMapper mapper
        ) {
            _groupRequestService = groupRequestService;
            Mapper = mapper;
        }

        public async Task<IEnumerable<GroupDto>> GetGroupList()
        {
            var result = await _groupRequestService.GetGroupList();
            var list = Mapper.Map<IEnumerable<Groups>, IEnumerable<GroupDto>>(result);
            return list;
        }

        public Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId)
        {
            throw new NotImplementedException();
        }
    }
}
