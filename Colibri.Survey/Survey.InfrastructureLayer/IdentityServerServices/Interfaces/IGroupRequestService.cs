using Survey.DomainModelLayer.Models;
using Survey.DomainModelLayer.Models.IdentityServer;
using Survey.DomainModelLayer.Models.IdentityServer.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Survey.InfrastructureLayer.IdentityServices
{
    public interface IGroupRequestService
    {
        Task<PageDataModel<GroupModel>> GetGroups(PageSearchEntryModel pageSearchEntry);
        Task<PageDataModel<GroupModel>> GetRootGroups(PageSearchEntryModel pageSearchEntry);



        //// group
        //Task<IEnumerable<Groups>> GetGroupList();
        //Task<PageDataModel<Groups>> GetGroupListRoot(PageSearchEntryModel pageSearchEntry);
        //Task<IEnumerable<Groups>> GetSubGroupList(Guid groupId);
        //Task<Groups> CreateGroupAsync(Groups group);
        //Task<Boolean> DeleteGroup(Guid groupId);
        //Task<Groups> GetGroup(Guid groupId);
        //Groups UpdateGroupt(Groups group);

        //// group members
        //Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emails);
        //Task<bool> DeleteMemberFromGroupAsync(Guid groupId, string userId);
    }
}
