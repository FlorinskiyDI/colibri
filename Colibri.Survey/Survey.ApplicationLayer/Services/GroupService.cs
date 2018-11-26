using AutoMapper;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Search;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using Survey.InfrastructureLayer.IdentityServices;
using System.Collections.Generic;
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
        )
        {
            _groupRequestService = groupRequestService;
            Mapper = mapper;
        }

        public async Task<SearchResultDto<GroupDto>> GetGroups(SearchQueryDto searchEntryDto)
        {
            var searchEntry = Mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetGroups(searchEntry);
            var list = Mapper.Map<SearchResultModel<GroupModel>, SearchResultDto<GroupDto>>(result);
            return list;
        }

        public async Task<SearchResultDto<GroupDto>> GetRootGroups(SearchQueryDto searchEntryDto)
        {
            var searchEntry = Mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetRootGroups(searchEntry);
            var list = Mapper.Map<SearchResultModel<GroupModel>, SearchResultDto<GroupDto>>(result);
            return list;
        }

        public async Task<IEnumerable<GroupDto>> GetSubgroups(SearchQueryDto searchEntryDto, string parentId)
        {
            var searchEntry = Mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetSubgroups(searchEntry, parentId);
            var list = Mapper.Map<IEnumerable<GroupModel>, IEnumerable<GroupDto>>(result);
            return list;
        }


        //public async Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId)
        //{
        //    var result = await _groupRequestService.GetSubGroupList(groupId);
        //    var list = Mapper.Map<IEnumerable<Groups>, IEnumerable<GroupDto>>(result);
        //    return list;
        //}

        //public async Task<GroupDto> CreateGroup(GroupDto groupDto)
        //{
        //    var group = Mapper.Map<GroupDto, Groups>(groupDto);
        //    var result = await _groupRequestService.CreateGroupAsync(group);
        //    groupDto = Mapper.Map<Groups, GroupDto>(result);
        //    return groupDto;
        //}

        //public GroupDto UpdateGroup(GroupDto groupDto)
        //{
        //    var group = Mapper.Map<GroupDto, Groups>(groupDto);
        //    var result = _groupRequestService.UpdateGroupt(group);
        //    groupDto = Mapper.Map<Groups, GroupDto>(result);
        //    return groupDto;
        //}

        //public async Task<bool> DeleteGroup(Guid groupId)
        //{
        //    var result = await _groupRequestService.DeleteGroup(groupId);
        //    return result;
        //}

        //public async Task<GroupDto> GetGroup(Guid groupId)
        //{
        //    var result = await _groupRequestService.GetGroup(groupId);
        //    var groupDto = Mapper.Map<Groups, GroupDto>(result);
        //    return groupDto;
        //}




        //#region group members

        //public async Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails)
        //{
        //    var result = await _groupRequestService.AddMembersToGroupAsync(groupId, emails);
        //    return result;
        //}

        //public async Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId)
        //{
        //    var result = await _groupRequestService.DeleteMemberFromGroupAsync(groupId, userId);
        //    return result;
        //}

        //#endregion
    }
}
