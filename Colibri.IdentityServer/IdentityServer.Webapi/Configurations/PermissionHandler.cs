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

            //var filterContext = context.Resource as AuthorizationFilterContext;
            //var response = filterContext?.HttpContext.Response;
            //response?.OnStarting(async () =>
            //{
            //    filterContext.HttpContext.Response.StatusCode = 401;
            //    //await response.Body.WriteAsync(message, 0, message.Length); only when you want to pass a message
            //});
            //return Task.CompletedTask;

            context.Fail();
            var filterContext = context.Resource as AuthorizationFilterContext;
            var Response = filterContext.HttpContext.Response;
            var message = Encoding.UTF8.GetBytes("You don't have permissions to perform the action on the selected resource.");
            Response.OnStarting(async () =>
            {
                filterContext.HttpContext.Response.StatusCode = 403;
                await Response.Body.WriteAsync(message, 0, message.Length);
            });

            return Task.CompletedTask;




            //context.Fail();
            //var Response = context.HttpContext.Response;
            //var message = Encoding.UTF8.GetBytes("ReCaptcha failed");
            //Response.OnStarting(async () =>
            //{
            //    filterContext.HttpContext.Response.StatusCode = 429;
            //    await Response.Body.WriteAsync(message, 0, message.Length);
            //});

            //context.Fail();
            //context.Succeed(requirement);
            //return Task.CompletedTask;
        }
    }
}
