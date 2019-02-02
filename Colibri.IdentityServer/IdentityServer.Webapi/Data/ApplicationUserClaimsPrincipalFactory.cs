using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class ApplicationUserClaimsPrincipalFactory<TUser, TRole> : UserClaimsPrincipalFactory<TUser, TRole>
    where TUser : ApplicationUser
    where TRole : ApplicationRole
    {
        public ApplicationUserClaimsPrincipalFactory(
            UserManager<TUser> userManager,
            RoleManager<TRole> roleManager,
            IOptions<IdentityOptions> optionsAccessor) : base(userManager, roleManager, optionsAccessor)
        {
            options = optionsAccessor.Value;
        }

        private IdentityOptions options;

        public override async Task<ClaimsPrincipal> CreateAsync(TUser user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var id = await GenerateClaimsAsync(user);
            return new ClaimsPrincipal(id);
        }

        //protected override async Task<ClaimsIdentity> GenerateClaimsAsync(TUser user)
        //{
        //    var id = await base.GenerateClaimsAsync(user);
        //    if (UserManager.SupportsUserRole)
        //    {
        //        var roles = await UserManager.GetRolesAsync(user);
        //        foreach (var roleName in roles)
        //        {
        //            id.AddClaim(new Claim(Options.ClaimsIdentity.RoleClaimType, roleName));
        //            if (RoleManager.SupportsRoleClaims)
        //            {
        //                var role = await RoleManager.FindByNameAsync(roleName);
        //                if (role != null)
        //                {
        //                    id.AddClaims(await RoleManager.GetClaimsAsync(role));
        //                }
        //            }
        //        }
        //    }
        //    return id;
        //}
    }
}
