using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IAppUserRepository
    {
        Task<IEnumerable<ApplicationUser>> GetAppUsersForGroup(Guid groupId);
    }
}
