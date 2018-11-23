using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos;
using IdentityServer.Webapi.Dtos.Pager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupService
    {

        Task<DataPage<GroupDto>> GetPageDataAsync(string userId, PageSearchEntry searchEntry, bool isRoot = false);
        Task<IEnumerable<GroupDto>> GetByParentIdAsync(string userId, SearchEntry searchEntry, string parentId);


        //Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId);
        //void SubscribeToGroupAsync(string userId, Guid groupId);
        //Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}
