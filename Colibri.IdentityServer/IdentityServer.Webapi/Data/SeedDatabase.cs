using IdentityServer.Webapi.Configurations;
using IdentityServer.Webapi.Configurations.AspNetIdentity;
using IdentityServer.Webapi.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
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
                await CreateRole(roleManager, logger, SystemRoleScopes.SUPER_ADMIN);
                await AddAllPermissionsToRole(roleManager, logger, SystemRoleScopes.SUPER_ADMIN);
                // Admin
                await CreateRole(roleManager, logger, SystemRoleScopes.ADMIN);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Users.List);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Users.Get);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Users.Invite);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Users.Update);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Users.Disable);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.Create);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.List);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.ListAll);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.ListRoot);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.Get);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.Update);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.Delete);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.GetSubgroups);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.GetRoles);
                // User
                //await CreateRole(roleManager, logger, SystemRoleScopes.USER);
                //await AddPermissionToRole(roleManager, logger, SystemRoleScopes.USER, SystemStaticPermissions.Users.Get);
                //await AddPermissionToRole(roleManager, logger, SystemRoleScopes.USER, SystemStaticPermissions.Users.Update);
                // GroupCreator
                await CreateRole(roleManager, logger, SystemRoleScopes.GROUP_CREATOR);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.GROUP_CREATOR, SystemStaticPermissions.Groups.Create);
                // GroupAdmin
                await CreateRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.Delete);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.List);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.ListAll);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.ListRoot);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.Get);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.Update);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.ADMIN, SystemStaticPermissions.Groups.GetSubgroups);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.ADMIN, SystemStaticPermissions.Groups.GetRoles);
                // GroupEditor
                await CreateRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.List);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.ListAll);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.ListRoot);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.Get);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.Update);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.EDITOR, SystemStaticPermissions.Groups.GetSubgroups);
                // GroupViewer
                await CreateRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER, SystemStaticPermissions.Groups.List);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER, SystemStaticPermissions.Groups.ListAll);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER, SystemStaticPermissions.Groups.ListRoot);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER, SystemStaticPermissions.Groups.Get);
                await AddPermissionToRole(roleManager, logger, SystemRoleScopes.Groups.VIEWER, SystemStaticPermissions.Groups.GetSubgroups);
            }

            if (!context.Users.Any())
            {
                // GroupViewer
                var groupViewer = await CreateDefaultUser(userManager, logger, "group_viewer@gmail.com", "groupviewer");
                await SetPasswordForUser(userManager, logger, "group_viewer@gmail.com", groupViewer, "groupviewer");
                await AddToRoleAsync(groupViewer, userManager, logger, SystemRoleScopes.Groups.VIEWER);
                // GroupEditor
                var groupEditor = await CreateDefaultUser(userManager, logger, "group_editor@gmail.com", "groupeditor");
                await SetPasswordForUser(userManager, logger, "group_editor@gmail.com", groupEditor, "groupeditor");
                await AddToRoleAsync(groupEditor, userManager, logger, SystemRoleScopes.Groups.EDITOR);
                // GroupAdmin
                var groupAdmin = await CreateDefaultUser(userManager, logger, "group_admin@gmail.com", "groupadmin");
                await SetPasswordForUser(userManager, logger, "group_admin@gmail.com", groupAdmin, "groupadmin");
                await AddToRoleAsync(groupAdmin, userManager, logger, SystemRoleScopes.Groups.ADMIN);
                // GroupCreator
                var groupCreator = await CreateDefaultUser(userManager, logger, "group_creator@gmail.com", "groupcreator");
                await SetPasswordForUser(userManager, logger, "group_creator@gmail.com", groupCreator, "groupcreator");
                await AddToRoleAsync(groupCreator , userManager, logger, SystemRoleScopes.GROUP_CREATOR);
                // User
                //var user = await CreateDefaultUser(userManager, logger, "user@gmail.com", "user");
                //await SetPasswordForUser(userManager, logger, "user@gmail.com", user, "user");
                //await AddToRoleAsync(user, userManager, logger, SystemRoleScopes.USER);
                // Admin
                var admin = await CreateDefaultUser(userManager, logger, "admin@gmail.com", "admin");
                await SetPasswordForUser(userManager, logger, "admin@gmail.com", admin, "admin");
                await AddToRoleAsync(admin, userManager, logger, SystemRoleScopes.SUPER_ADMIN);
                // SuperAdmin
                var sadmin = await CreateDefaultUser(userManager, logger, "superadmin@gmail.com", "sadmin");
                await SetPasswordForUser(userManager, logger, "superadmin@gmail.com", sadmin, "sadmin");
                await AddToRoleAsync(sadmin, userManager, logger, SystemRoleScopes.SUPER_ADMIN);




                //// GroupAdmin
                //var groupadmin= await CreateDefaultUser(userManager, logger, "groupadmin@gmail.com", "groupadmin");
                //await SetPasswordForUser(userManager, logger, "groupadmin@gmail.com", groupadmin, "groupadmin");
                //await AddToRoleAsync(groupadmin, userManager, logger, "GroupAdmin");
                //// GroupOwner
                //var groupowner = await CreateDefaultUser(userManager, logger, "groupowner@gmail.com", "groupowner");
                //await SetPasswordForUser(userManager, logger, "groupowner@gmail.com", groupowner, "groupowner");
                //await AddToRoleAsync(groupowner, userManager, logger, "GroupOwner");
                //// GroupCreator
                //var groupcreator= await CreateDefaultUser(userManager, logger, "groupcreator@gmail.com", "groupcreator");
                //await SetPasswordForUser(userManager, logger, "groupcreator@gmail.com", groupcreator, "groupcreator");
                //await AddToRoleAsync(groupcreator, userManager, logger, "GroupCreator");
                //// GroupEditor
                //var groupeditor = await CreateDefaultUser(userManager, logger, "groupeditor@gmail.com", "groupeditor");
                //await SetPasswordForUser(userManager, logger, "groupeditor@gmail.com", groupeditor, "groupeditor");
                //await AddToRoleAsync(groupeditor, userManager, logger, "GroupEditor");
                //// GroupViewer
                //var groupviwer = await CreateDefaultUser(userManager, logger, "groupviwer@gmail.com", "groupviwer");
                //await SetPasswordForUser(userManager, logger, "groupviwer@gmail.com", groupviwer, "groupviwer");
                //await AddToRoleAsync(groupviwer, userManager, logger, "GroupViewer");
            }
        }

        private static async Task AddAllPermissionsToRole(RoleManager<ApplicationRole> roleManager, ILogger<DbInitializer> logger, string role)
        {
            var permissionList = new List<string>();
            permissionList.AddRange(ClassHelper.GetConstantValues<SystemStaticPermissions.Configs>());
            permissionList.AddRange(ClassHelper.GetConstantValues<SystemStaticPermissions.Users>());
            permissionList.AddRange(ClassHelper.GetConstantValues<SystemStaticPermissions.Groups>());
            //var permissionList
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
