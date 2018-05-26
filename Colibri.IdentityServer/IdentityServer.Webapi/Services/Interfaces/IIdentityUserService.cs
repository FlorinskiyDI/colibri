using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services
{
    public interface IIdentityUserService
    {
        Task<bool> AddMembersFroupAsync(Guid groupId, List<string> emails);
    }
}
