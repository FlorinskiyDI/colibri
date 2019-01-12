using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUserStore : UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>>
    {
        public ApplicationUserStore(ApplicationDbContext context, IdentityErrorDescriber describer = null)
            : base(context, describer)
        {
        }
        //public override Task AddToRoleAsync(ApplicationUser user, string normalizedRoleName, CancellationToken cancellationToken = default)
        //{
        //    return base.AddToRoleAsync(user, normalizedRoleName, cancellationToken);
        //}

        public Task AddToRoleAsync(ApplicationUser user,  String normalizedRoleName, Guid? groupId = null, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();

            // Checking user
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName));
            }

            // Checking role
            ApplicationRole roleEntity = null;
            try
            {
                roleEntity = Context.Set<ApplicationRole>().Where(r => r.Name == normalizedRoleName).Single();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            if (roleEntity == null)
            {
                throw new InvalidOperationException($"Role `{normalizedRoleName}` not found");
            }

            // Checking group
            Groups groupEntity = null;
            if (groupId != null)
            {
                try
                {
                    groupEntity = Context.Set<Groups>().Where(r => r.Id == groupId).Single();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                if (groupEntity == null)
                {
                    throw new InvalidOperationException($"Group `{groupId}` not found");
                }
            }

            Context.Set<ApplicationUserRole>().Add(new ApplicationUserRole
            {
                GroupId = groupEntity?.Id,
                RoleId = roleEntity.Id,
                UserId = user.Id
            });

            return Context.SaveChangesAsync();
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, Groups group, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (string.IsNullOrWhiteSpace(normalizedRoleName))
            {
                throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName));
            }
            var role = await FindRoleAsync(normalizedRoleName, cancellationToken);
            if (role != null)
            {
                var userRole = await FindUserRoleAsync(user.Id, role.Id, cancellationToken);
                return userRole != null;
            }
            return false;
        }


    }
}
