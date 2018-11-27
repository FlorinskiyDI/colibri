using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IUserGroupService
    {
        Task<ApplicationUserGroups> AddUserToGroup(ApplicationUserGroups model);
    }
}
