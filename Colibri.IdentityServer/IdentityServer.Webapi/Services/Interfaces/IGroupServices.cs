using dataaccesscore.EFCore.Paging;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos.Pager;
using System;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupServices
    {
        Task<DataPage<Groups, Guid>> GetRootAsync(PageSearchEntry searchEntry, string userId);
        void SubscribeToGroupAsync(string userId, Guid groupId);
        Task UnsubscribeToGroup(string userId, Guid groupId);
    }
}
