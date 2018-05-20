using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class AppUserRepository : IAppUserRepository
    {
        public async Task<IEnumerable<ApplicationUser>> GetAppUsersForGroup(Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                    .Where(c => c.GroupId == groupId)
                    .Select(c => c.User)
                    .ToListAsync();
            }
        }

    }
}
