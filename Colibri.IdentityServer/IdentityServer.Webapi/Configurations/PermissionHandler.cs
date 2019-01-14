using IdentityServer.Webapi.Configurations.AspNetIdentity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>, IAuthorizationHandler
    {

        public PermissionHandler()
        {
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {

            var claims = context.User.Claims.ToList();
            var permissionClaims = claims.Where(item => item.Type == CustomClaimValueTypes.Permission).ToList();
            var permission = permissionClaims.Where(per => per.Value == requirement.Permission).FirstOrDefault();

            if (permission != null)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                var filterContext = context.Resource as AuthorizationFilterContext;
                var Response = filterContext.HttpContext.Response;
                var message = Encoding.UTF8.GetBytes("You don't have permissions to perform the action on the selected resource.");
                Response.OnStarting(async () =>
                {
                    filterContext.HttpContext.Response.StatusCode = 403;
                    await Response.Body.WriteAsync(message, 0, message.Length);
                });
            }



            return Task.CompletedTask;
        }
    }
}
