using IdentityServer.Webapi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IGroupNodeService
    {
        Task<IEnumerable<GroupNode>> GetAncestors(Guid descendantId);
        Task AddPathsBetweenDescendantAndAncestors(Guid newDescendantId, Guid? parentId);
    }
}
