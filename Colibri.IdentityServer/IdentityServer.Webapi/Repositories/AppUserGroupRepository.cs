using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class AppUserGroupRepository: IAppUserGroupRepository
    {
        public async Task<ApplicationUserGroups> CreateAppUserGroupAsync(ApplicationUserGroups appUserGroup)
        {
            using (var ctx = new ApplicationDbContext())
            {
                await ctx.Set<ApplicationUserGroups>().AddAsync(appUserGroup);
                await ctx.SaveChangesAsync();
            }
            return appUserGroup;
        }

        public async Task<ApplicationUserGroups> GetAppUserGroupAsync(string userId, Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var userGroup = await ctx.Set<ApplicationUserGroups>().Where(c => c.UserId == userId && c.GroupId == groupId).FirstOrDefaultAsync();
                return userGroup;
            }
        }


        public void DeleteAppUserGroupByGroupAsync(Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Set<ApplicationUserGroups>().RemoveRange(ctx.Set<ApplicationUserGroups>().Where(x => x.GroupId == groupId));
                ctx.SaveChanges();
            }
        }

        public void DeleteAppUserGroupAsync(ApplicationUserGroups userGroup)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Set<ApplicationUserGroups>().Remove(userGroup);
                ctx.SaveChanges();
            }
        }
    }
}
