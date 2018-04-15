using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class GroupRepository: IGroupRepository
    {
        
        public virtual async Task<IEnumerable<Groups>> GetAllAsync()
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<Groups>().ToArrayAsync();
            }
        }

        public virtual async Task<Groups> GetAsync(Guid id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =  await ctx.Set<Groups>()
                    .Where(i => i.Id == id)
                   .Include(x => x.GroupNodesOffspring)
                    .SingleOrDefaultAsync();
                return entity;
            }
        }
    }
}
