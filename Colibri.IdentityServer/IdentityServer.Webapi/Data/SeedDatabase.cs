using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Configurations.AspNetIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

// SuperAdmin
// GroupOwner
// GroupEditor
// GroupViewer

namespace IdentityServer.Webapi.Data
{
    public class DbInitializer
    {
        public DbInitializer() { }

        public static async Task SeedingAsync(ApplicationUserManager userManager, RoleManager<ApplicationRole> roleManager, ApplicationDbContext context, ILogger<DbInitializer> logger)
        {
            if (!context.Roles.Any())
            {
                // SuperAdmin
                await CreateRole(roleManager, logger, "SuperAdmin");
                await AddAllPermissionsToRole(roleManager, logger, "SuperAdmin");
                // GroupAdmin
                await CreateRole(roleManager, logger, "GroupAdmin");
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupCreate);
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupDelete);
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupGet);
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupGetSubgroups);
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupList);
                await AddPermissionToRole(roleManager, logger, "GroupAdmin", SystemPermissionScope.GroupUpdate);
                // GroupOwner
                await CreateRole(roleManager, logger, "GroupOwner");
                await AddPermissionToRole(roleManager, logger, "GroupOwner", SystemPermissionScope.GroupDelete);
                await AddPermissionToRole(roleManager, logger, "GroupOwner", SystemPermissionScope.GroupGet);
                await AddPermissionToRole(roleManager, logger, "GroupOwner", SystemPermissionScope.GroupGetSubgroups);
                await AddPermissionToRole(roleManager, logger, "GroupOwner", SystemPermissionScope.GroupList);
                await AddPermissionToRole(roleManager, logger, "GroupOwner", SystemPermissionScope.GroupUpdate);
                // GroupCreator
                await CreateRole(roleManager, logger, "GroupCreator");
                await AddPermissionToRole(roleManager, logger, "GroupCreator", SystemPermissionScope.GroupCreate);
                // GroupEditor
                await CreateRole(roleManager, logger, "GroupEditor");
                await AddPermissionToRole(roleManager, logger, "GroupEditor", SystemPermissionScope.GroupGet);
                await AddPermissionToRole(roleManager, logger, "GroupEditor", SystemPermissionScope.GroupGetSubgroups);
                await AddPermissionToRole(roleManager, logger, "GroupEditor", SystemPermissionScope.GroupList);
                await AddPermissionToRole(roleManager, logger, "GroupEditor", SystemPermissionScope.GroupUpdate);
                // GroupViewer
                await CreateRole(roleManager, logger, "GroupViewer");
                await AddPermissionToRole(roleManager, logger, "GroupViewer", SystemPermissionScope.GroupGet);
                await AddPermissionToRole(roleManager, logger, "GroupViewer", SystemPermissionScope.GroupGetSubgroups);
                await AddPermissionToRole(roleManager, logger, "GroupViewer", SystemPermissionScope.GroupList);
                
            }
            if (!context.Users.Any())
            {
                // SuperAdmin
                var user = await CreateDefaultUser(userManager, logger, "superadmin@gmail.com", "sadmin");
                await SetPasswordForUser(userManager, logger, "superadmin@gmail.com", user, "sadmin");
                await AddToRoleAsync(user, userManager, logger, "SuperAdmin");
                // GroupAdmin
                var groupadmin= await CreateDefaultUser(userManager, logger, "groupadmin@gmail.com", "groupadmin");
                await SetPasswordForUser(userManager, logger, "groupadmin@gmail.com", groupadmin, "groupadmin");
                await AddToRoleAsync(groupadmin, userManager, logger, "GroupAdmin");
                // GroupOwner
                var groupowner = await CreateDefaultUser(userManager, logger, "groupowner@gmail.com", "groupowner");
                await SetPasswordForUser(userManager, logger, "groupowner@gmail.com", groupowner, "groupowner");
                await AddToRoleAsync(groupowner, userManager, logger, "GroupOwner");
                // GroupCreator
                var groupcreator= await CreateDefaultUser(userManager, logger, "groupcreator@gmail.com", "groupcreator");
                await SetPasswordForUser(userManager, logger, "groupcreator@gmail.com", groupcreator, "groupcreator");
                await AddToRoleAsync(groupcreator, userManager, logger, "GroupCreator");
                // GroupEditor
                var groupeditor = await CreateDefaultUser(userManager, logger, "groupeditor@gmail.com", "groupeditor");
                await SetPasswordForUser(userManager, logger, "groupeditor@gmail.com", groupeditor, "groupeditor");
                await AddToRoleAsync(groupeditor, userManager, logger, "GroupEditor");
                // GroupViewer
                var groupviwer = await CreateDefaultUser(userManager, logger, "groupviwer@gmail.com", "groupviwer");
                await SetPasswordForUser(userManager, logger, "groupviwer@gmail.com", groupviwer, "groupviwer");
                await AddToRoleAsync(groupviwer, userManager, logger, "GroupViewer");
            }
        }

        private static async Task AddAllPermissionsToRole(RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger, string role)
        {
            var permissionList = SystemPermissionScope.GetValues();
            var roleEntity = await roleManager.FindByNameAsync(role);
            foreach (var per in permissionList)
            {
                await roleManager.AddClaimAsync(roleEntity, new Claim(CustomClaimValueTypes.Permission, per));
            }

        }

        private static async Task AddPermissionToRole(RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger, string role, string permission)
        {
            var roleEntity = await roleManager.FindByNameAsync(role);
            await roleManager.AddClaimAsync(roleEntity, new Claim(CustomClaimValueTypes.Permission, permission));
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
