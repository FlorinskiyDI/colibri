using IdentityServer.Webapi.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        private readonly ApplicationUserStore _applicationUserStore;
        public ApplicationUserManager(
            UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid, IdentityUserClaim<Guid>, ApplicationUserRole, IdentityUserLogin<Guid>, IdentityUserToken<Guid>, IdentityRoleClaim<Guid>> userStore,
            IUserStore<ApplicationUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<ApplicationUser> passwordHasher, IEnumerable<IUserValidator<ApplicationUser>> userValidators,
            IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider serviceProvider,
            ILogger<UserManager<ApplicationUser>> logger)
            : base(
                store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors,
                serviceProvider, logger)
        {
            _applicationUserStore = userStore as ApplicationUserStore;
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser user, string role, Guid groupId)
        {
            ThrowIfDisposed();
            //var userRoleStore = Store as IUserRoleStore<ApplicationUser>;
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var normalizedRole = NormalizeKey(role);
            if (await _applicationUserStore.IsInRoleAsync(user, groupId, normalizedRole, CancellationToken))
            {
                var errors = new IdentityError();
                return IdentityResult.Failed(new IdentityError() { Description = $"User `{user.UserName}` with group id `{groupId}` already have role `{role}`" } );
            }
            await _applicationUserStore.AddToRoleAsync(user, normalizedRole, groupId, CancellationToken);
            return await UpdateUserAsync(user);
        }

    }
}
