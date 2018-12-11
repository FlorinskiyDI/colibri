using AutoMapper;
using Survey.ApplicationLayer.Dtos.Models;
using Survey.ApplicationLayer.Dtos.Search;
using Survey.ApplicationLayer.Services.Interfaces;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.Search;
using Survey.InfrastructureLayer.IdentityServices;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Survey.ApplicationLayer.Services
{
    public class GroupService : IGroupService
    {
        private readonly IGroupRequestService _groupRequestService;
        protected readonly IMapper _mapper;

        public GroupService(
            IGroupRequestService groupRequestService,
            IMapper mapper
        )
        {
            _groupRequestService = groupRequestService;
            _mapper = mapper;
        }

        public async Task<SearchResultDto<GroupDto>> GetGroups(SearchQueryDto searchEntryDto)
        {
            var searchEntry = _mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetGroups(searchEntry);
            var list = _mapper.Map<SearchResultModel<GroupModel>, SearchResultDto<GroupDto>>(result);
            return list;
        }

        public async Task<SearchResultDto<GroupDto>> GetRootGroups(SearchQueryDto searchEntryDto)
        {
            var searchEntry = _mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetRootGroups(searchEntry);
            var list = _mapper.Map<SearchResultModel<GroupModel>, SearchResultDto<GroupDto>>(result);
            return list;
        }

        public async Task<IEnumerable<GroupDto>> GetSubgroups(SearchQueryDto searchEntryDto, string parentId)
        {
            var searchEntry = _mapper.Map<SearchQueryDto, SearchQueryModel>(searchEntryDto);
            var result = await _groupRequestService.GetSubgroups(searchEntry, parentId);
            var list = _mapper.Map<IEnumerable<GroupModel>, IEnumerable<GroupDto>>(result);
            return list;
        }

        public async Task<GroupDto> CreateGroup(GroupDto modelDto)
        {
            var model = _mapper.Map<GroupDto, GroupModel>(modelDto);
            var result = await _groupRequestService.CreateGroupAsync(model);
            var value = _mapper.Map<GroupModel, GroupDto>(result);
            return value;
        }

        public async Task DeleteGroup(string groupId)
        {
            await _groupRequestService.DeleteGroupAsync(groupId);
            return;
        }

        public async Task<GroupDto> GetGroup(Guid groupId)
        {
            var result = await _groupRequestService.GetGroup(groupId);
            var value = _mapper.Map<GroupModel, GroupDto>(result);
            return value;
        }

        public async Task<GroupDto> UpdateGroup(GroupDto model)
        {
            var entity = _mapper.Map<GroupDto, GroupModel>(model);
            var result = await _groupRequestService.UpdateGroup(entity);
            model = _mapper.Map<GroupModel, GroupDto>(result);
            return model;
        }


        //public async Task<IEnumerable<GroupDto>> GetSubGroupList(Guid groupId)
        //{
        //    var result = await _groupRequestService.GetSubGroupList(groupId);
        //    var list = _mapper.Map<IEnumerable<Groups>, IEnumerable<GroupDto>>(result);
        //    return list;
        //}

        //public async Task<GroupDto> CreateGroup(GroupDto groupDto)
        //{
        //    var group = _mapper.Map<GroupDto, Groups>(groupDto);
        //    var result = await _groupRequestService.CreateGroupAsync(group);
        //    groupDto = _mapper.Map<Groups, GroupDto>(result);
        //    return groupDto;
        //}

        //public GroupDto UpdateGroup(GroupDto groupDto)
        //{
        //    var group = _mapper.Map<GroupDto, Groups>(groupDto);
        //    var result = _groupRequestService.UpdateGroupt(group);
        //    groupDto = _mapper.Map<Groups, GroupDto>(result);
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
        //    var groupDto = _mapper.Map<Groups, GroupDto>(result);
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
