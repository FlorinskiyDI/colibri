using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Webapi.Services
{
    public class GroupMemberService : IGroupMemberService
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAppUserService _appUserService;
        private readonly IGroupService _groupServices;
        public GroupMemberService(
            IAppUserService appUserService,
            IAppUserRepository appUserRepository,
            IGroupService groupServices
        )
        {
            _appUserService = appUserService;
            _appUserRepository = appUserRepository;
            _groupServices = groupServices;
        }
        
        public async Task<bool> AddMembersToGroupAsync(Guid groupId, List<string> emailList)
        {
            foreach (var email in emailList)
            {
                var user = await _appUserService.AddUserByEmailWithoutPassword(email);
                //_groupServices.SubscribeToGroupAsync(user.Id, groupId);
            }
            //
            return true;
        }

        public async Task<IEnumerable<ApplicationUser>> GetMembersForGroupAsync(Guid groupId)
        {
            var list = await _appUserRepository.GetAppUsersForGroup(groupId);
            //
            return list;
        }

        public async Task DeleteMember(string userId, Guid groupId)
        {
            //await _groupServices.UnsubscribeToGroup(userId, groupId);
            return;
        }
    }
}
