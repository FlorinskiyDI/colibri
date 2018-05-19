using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class GroupRepository : IGroupRepository
    {

        public async Task<IEnumerable<Groups>> GetAllAsync(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                    .Where(c => c.UserId == userId).Select(c => c.Group)
                    .Where(c => c.ParentId == null).ToListAsync();
            }
        }

        public async Task<IEnumerable<Groups>> GetSubGroupsAsync(Guid? groupId, string userId)
        {
            groupId = Guid.Empty == groupId ? null : groupId;
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                .Where(c => c.UserId == userId).Select(c => c.Group)
                .Where(c => c.ParentId == groupId).ToListAsync();
            }
        }

        public async Task<Groups> CreateGroupAsync(Groups group)
        {
            using (var ctx = new ApplicationDbContext())
            {
                await ctx.Set<Groups>().AddAsync(group);
                await ctx.SaveChangesAsync();
            }
            return group;
        }

        public void DeleteGroup(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Set<Groups>().Where(i => i.Id == id).SingleOrDefault();
                ctx.Set<Groups>().Remove(entity);
                ctx.SaveChanges();
            }
        }

        public async Task<Groups> GetAsync(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = await ctx.Set<Groups>()
                    .Where(i => i.Id == id)
                    .SingleOrDefaultAsync();
                return entity;
            }
        }
    }
}
