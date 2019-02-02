using dataaccesscore.EFCore.Models;
using dataaccesscore.EFCore.Repositories;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Dtos.Views;
using IdentityServer.Webapi.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Repositories
{
    public class AppUserRepository : BaseRepository<ApplicationDbContext, Groups>, IAppUserRepository
    {
        public AppUserRepository(ILogger<LoggerDataAccess> logger)
        : base(logger, null)
        {
        }

        public async Task<IEnumerable<ApplicationUser>> GetAppUsersForGroup(Guid groupId)
        {
            using (var ctx = new ApplicationDbContext())
            {
                return await ctx.Set<MemberGroups>()
                    .Where(c => c.GroupId == groupId)
                    .Select(c => c.User)
                    .ToListAsync();
            }
        }

        public async Task<UserFullDetailsViewModel> GetUserFullDetails(string userId)
        {
            UserFullDetailsViewModel result = new UserFullDetailsViewModel();
            using (var ctx = new ApplicationDbContext())
            {
                var data = await ctx.Set<ApplicationUser>()
                    .Include(c => c.MemberGroups).ThenInclude(d => d.Group)
                    .FirstOrDefaultAsync(c => c.Id == new Guid(userId));

                result = new UserFullDetailsViewModel()
                {
                    Id =  data.Id.ToString(),
                    UserName = data.UserName,
                    Email = data.Email,
                    EmailConfirmed = data.EmailConfirmed,
                    EmailConfirmTokenLifespan = data.EmailConfirmTokenLifespan,
                    EmailConfirmInvitationDate = data.EmailConfirmInvitationDate,
                    GroupsToMember = data.MemberGroups.Select(d => d.Group).ToList()
                };
            }
            return result;
        }

    }
}
