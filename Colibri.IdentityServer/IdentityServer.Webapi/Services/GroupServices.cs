using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer.Webapi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public class GroupServices : IGroupServices
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IAppUserGroupRepository _appUserGroupRepository;
        public GroupServices(
            IAppUserGroupRepository appUserGroupRepository,
            IGroupRepository groupRepository
        )
        {
            _groupRepository = groupRepository;
            _appUserGroupRepository = appUserGroupRepository;
        }

        public async void SubscribeToGroupAsync(string userId, Guid groupId)
        {
            var userGroup = await _appUserGroupRepository.GetAppUserGroupAsync(userId, groupId);
            if (userGroup == null)
            {
                await _appUserGroupRepository.CreateAppUserGroupAsync(new ApplicationUserGroups()
                {
                    GroupId = groupId,
                    UserId = userId
                });
            }
        }

        public async Task UnsubscribeToGroup(string userId, Guid groupId)
        {
            var userGroup = await _appUserGroupRepository.GetAppUserGroupAsync(userId, groupId);
            if (userGroup == null)
            {
                throw new ArgumentException("not found userGroup");
            }
            _appUserGroupRepository.DeleteAppUserGroupAsync(userGroup);

            return;
        }
    }
}
