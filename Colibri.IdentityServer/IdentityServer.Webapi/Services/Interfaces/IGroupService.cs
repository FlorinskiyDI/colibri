using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos.Pager;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupService
    {

        Task<DataPage<Groups, Guid>> GetPageDataAsync(string userId, PageSearchEntry searchEntry, bool isRoot = false);
        Task<List<Groups>> GetByParentIdAsync(string userId, string parentId);


        //Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId);
        //void SubscribeToGroupAsync(string userId, Guid groupId);
        //Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}
