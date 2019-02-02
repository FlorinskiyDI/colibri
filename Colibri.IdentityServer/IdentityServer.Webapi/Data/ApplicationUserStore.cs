using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

        public Task AddToRoleAsync(ApplicationUser user, String normalizedRoleName, Guid? groupId = null, CancellationToken cancellationToken = default)
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
            try
            {
                Context.Set<ApplicationUserRole>().AddAsync(new ApplicationUserRole
                {
                    GroupId = groupEntity.Id,
                    RoleId = roleEntity.Id,
                    UserId = user.Id
                });
            }
            catch (Exception ex)
            {

                throw;
            }
            

            return Context.SaveChangesAsync();
        }

        //protected  Task<ApplicationRole> FindRoleByGroupAsync(Guid roleId, CancellationToken cancellationToken)
        //{
        //    return Context.ApplicationUserRoles.SingleOrDefaultAsync(r => r.RoleId == normalizedRoleName && r. ,  cancellationToken);
        //}

        protected async Task<ApplicationUserRole> FindUserRoleGroupAsync(Guid userId, Guid roleId, Guid groupId, CancellationToken cancellationToken)
        {
            var obj = new object[] { userId, roleId, groupId };
            var result = await Context.ApplicationUserRoles.Where(c => c.RoleId == roleId && c.UserId == userId && c.GroupId == groupId).FirstOrDefaultAsync();
            return result;
        }

        public async Task<bool> IsInRoleAsync(ApplicationUser user, Guid groupId, string normalizedRoleName, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (groupId == null) { throw new ArgumentNullException(nameof(groupId)); }
            if (user == null) { throw new ArgumentNullException(nameof(user)); }
            if (string.IsNullOrWhiteSpace(normalizedRoleName)) { throw new ArgumentException("Value cannot be null or empty", nameof(normalizedRoleName)); }

            var role = await FindRoleAsync(normalizedRoleName, cancellationToken);
            if (role != null)
            {
                var userRole = await FindUserRoleGroupAsync(user.Id, role.Id, groupId, cancellationToken);
                return userRole != null;
            }
            return false;
        }

        public override async Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            ThrowIfDisposed();
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var userId = user.Id;
            var query = from userRole in Context.ApplicationUserRoles
                        join role in Context.ApplicationRoles on userRole.RoleId equals role.Id
                        where userRole.UserId.Equals(userId) && userRole.GroupId == null
                        select role.Name;
            var result = await query.ToListAsync(cancellationToken);
            return result;
        }

        //public async Task<IList<string>> GetRolesByGroupAsync(ApplicationUser user, Guid groupId, CancellationToken cancellationToken = default)
        //{
        //    cancellationToken.ThrowIfCancellationRequested();
        //    ThrowIfDisposed();
        //    if (user == null)
        //    {
        //        throw new ArgumentNullException(nameof(user));
        //    }
        //    var userId = user.Id;
        //    var query = from userRole in Context.ApplicationUserRoles
        //                join role in Context.ApplicationRoles on userRole.RoleId equals role.Id
        //                where userRole.UserId.Equals(userId) && userRole.GroupId == groupId
        //                select role.Name;
        //    var result = await query.ToListAsync(cancellationToken);
        //    return result;
        //}

    }
}
