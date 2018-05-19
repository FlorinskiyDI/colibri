using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IAppUserGroupRepository
    {
        Task<ApplicationUserGroups> CreateAppUserGroupAsync(ApplicationUserGroups appUserGroup);
        void DeleteAppUserGroupByGroupAsync(Guid groupId);
    }
}
