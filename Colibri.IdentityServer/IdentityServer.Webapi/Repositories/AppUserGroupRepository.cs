using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
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
        

        public void DeleteAppUserGroupByGroupAsync(Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Set<ApplicationUserGroups>().RemoveRange(ctx.Set<ApplicationUserGroups>().Where(x => x.GroupId == groupId));
                ctx.SaveChanges();
            }
        }
    }
}
