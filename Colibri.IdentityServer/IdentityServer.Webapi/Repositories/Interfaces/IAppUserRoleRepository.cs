using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories.Interfaces
{
    public interface IAppUserRoleRepository
    {
        Task<IList<ApplicationRole>> GetRolesByGroupAsync(Guid userId, Guid groupId);
    }
}
