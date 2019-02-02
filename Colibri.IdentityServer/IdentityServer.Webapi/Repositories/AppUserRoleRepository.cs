using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class AppUserRoleRepository : IAppUserRoleRepository
    {

        public async Task<IList<ApplicationRole>> GetRolesByGroupAsync(Guid userId, Guid groupId)
        {
            var result = new List<ApplicationRole>();
            using (var ctx = new ApplicationDbContext())
            {
                if (userId == null)
                {
                    throw new ArgumentNullException(nameof(userId));
                }
                var query = from userRole in ctx.ApplicationUserRoles
                            join role in ctx.ApplicationRoles on userRole.RoleId equals role.Id
                            where userRole.UserId.Equals(userId) && userRole.GroupId == groupId
                            select role;
                result = await query.ToListAsync();
            }
            return result;
        }


    }
}
