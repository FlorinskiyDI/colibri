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
                return await ctx.Set<ApplicationUserGroups>()
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
                    .Include(c => c.ApplicationUserGroups).ThenInclude(d => d.Group)
                    .Include(c => c.MemberGroups).ThenInclude(d => d.Group)
                    .FirstOrDefaultAsync(c => c.Id == userId);

                result = new UserFullDetailsViewModel()
                {
                    Id = data.Id,
                    UserName = data.UserName,
                    Email = data.Email,
                    EmailConfirmed = data.EmailConfirmed,
                    EmailConfirmTokenLifespan = data.EmailConfirmTokenLifespan,
                    EmailConfirmInvitationDate = data.EmailConfirmInvitationDate,
                    GroupsToManage = data.ApplicationUserGroups.Select(d => d.Group).ToList(),
                    GroupsToMember = data.MemberGroups.Select(d => d.Group).ToList()
                };
            }
            return result;
        }

    }
}
