using dataaccesscore.EFCore.Models;
using dataaccesscore.EFCore.Repositories;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class GroupRepository : BaseRepository<ApplicationDbContext, Groups>, IGroupRepository
    {
        public GroupRepository(ILogger<LoggerDataAccess> logger)
        : base(logger, null)
        {
        }

        public async Task<IEnumerable<Groups>> GetRootWithInverseAsync(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                    .Where(c => c.UserId == userId)
                    .Select(c => c.Group).Include(v => v.InverseParent)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Groups>> GetRootAsync(string userId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<ApplicationUserGroups>()
                    .Where(c => c.UserId == userId)
                    .Select(c => c.Group)
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<Groups>> GetSubGroupsAsync(Guid? groupId)
        {
            groupId = Guid.Empty == groupId ? null : groupId;
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<Groups>()
                    .Where(c => c.ParentId == groupId)
                    .ToListAsync();
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

        public Groups UpdateGroup(Groups group)
        {
            using (var ctx = new ApplicationDbContext())
            {
                ctx.Set<Groups>().Update(group);
                ctx.SaveChanges();
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
