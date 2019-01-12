using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Data
{
    public class DbInitializer
    {
        public DbInitializer() { }

        public static async Task SeedingAsync(ApplicationUserManager userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            if (!context.Roles.Any())
            {
                await CreateRole(roleManager, logger, "SuperAdmin");
                await CreateRole(roleManager, logger, "GroupAdmin");

            }
            if (!context.Users.Any())
            {
                var user = await CreateDefaultUser(userManager, logger, "superadmin@gmail.com", "sadmin");
                await SetPasswordForUser(userManager, logger, "superadmin@gmail.com", user, "sadmin");
                await AddToRoleAsync(user, userManager, logger, "SuperAdmin");
            }
        }

        private static async Task CreateRole(RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger, string role)
        {
            logger.LogInformation($"Create the role `{role}` for application");
            IdentityResult result = await roleManager.CreateAsync(new ApplicationRole(role));
            if (result.Succeeded)
            {
                logger.LogDebug($"Created the role `{role}` successfully");
            }
            else
            {
                ApplicationException exception = new ApplicationException($"Default role `{role}` cannot be created");
                //logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                throw exception;
            }
        }

        private static async Task AddToRoleAsync(ApplicationUser user, ApplicationUserManager userManager, ILogger<DbInitializer> logger, string role, Guid? group = null)
        {
            IdentityResult result = await userManager.AddToRoleAsync(user, role, group);
            if (result.Succeeded)
            {
                logger.LogDebug($"Added the role `{role}` to user successfully");
            }
            else
            {
                ApplicationException exception = new ApplicationException($"Role `{role}` cannot be added to user `{user.UserName}`");
                //logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(result));
                throw exception;
            }
        }

        private static async Task<ApplicationUser> CreateDefaultUser(UserManager<ApplicationUser> userManager, ILogger<DbInitializer> logger, string email, string userName)
        {
            logger.LogInformation($"Create default user with email `{email}` for application");

            ApplicationUser user = new ApplicationUser
            {

                UserName = userName,
                Email = email,
                EmailConfirmed = true
            };

            IdentityResult identityResult = await userManager.CreateAsync(user);

            if (identityResult.Succeeded)
            {
                logger.LogDebug($"Created default user `{email}` successfully");
            }
            else
            {
                ApplicationException exception = new ApplicationException($"Default user `{email}` cannot be created");
                //logger.LogError(exception, GetIdentiryErrorsInCommaSeperatedList(identityResult));
                throw exception;
            }

            ApplicationUser createdUser = await userManager.FindByEmailAsync(email);
            return createdUser;
        }

        private static async Task SetPasswordForUser(UserManager<ApplicationUser> userManager, ILogger<DbInitializer> logger, string email, ApplicationUser user, string password)
        {
            logger.LogInformation($"Set password for default user `{email}`");
            IdentityResult identityResult = await userManager.AddPasswordAsync(user, password);
            if (identityResult.Succeeded)
            {
                logger.LogTrace($"Set password `{password}` for default user `{email}` successfully");
            }
            else
            {
                ApplicationException exception = new ApplicationException($"Password for the user `{email}` cannot be set");
                throw exception;
            }
        }


    }
}
