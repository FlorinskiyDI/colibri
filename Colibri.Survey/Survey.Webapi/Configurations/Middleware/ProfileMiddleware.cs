using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Survey.Common.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Survey.Webapi.Configurations.Middleware
{
    public class ProfileMiddleWare
    {
        private RequestDelegate next;

        public ProfileMiddleWare(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                this.SetNTContext(context);
            }

            await this.next(context);
        }

        public void SetNTContext(HttpContext context)
        {
            var claims = context.User.Claims;
            //string companyId = context.Request.Headers[ExtJwtClaimTypes.TenantName];
            //companyId = companyId ?? claims.Where(c => c.Type == ExtJwtClaimTypes.TenantName).Select(c => c.Value).FirstOrDefault();
            //string userName = context.User.Identity.Name;
            //string userId = claims.First(c => c.Type == ExtJwtClaimTypes.sub).Value;
            //string firstName = claims.First(c => c.Type == JwtClaimTypes.Name).Value;
            //string lastName = claims.First(c => c.Type == JwtClaimTypes.FamilyName).Value;
            //string tenantName = claims.First(c => c.Type == ExtJwtClaimTypes.TenantName).Value;
            //int tenantId = Convert.ToInt32(claims.First(c => c.Type == ExtJwtClaimTypes.TenantId).Value);



            //string userId = claims.First(c => c.Type == ExtJwtClaimTypes.sub).Value;
            //string userName = claims.First(c => c.Type == ExtJwtClaimTypes.given_name).Value;
            //string userEmail = claims.First(c => c.Type == ExtJwtClaimTypes.email).Value;
            //string identityUrl = claims.First(c => c.Type == ExtJwtClaimTypes.iss).Value;
            //string identityUserToken = context.GetTokenAsync("access_token").Result;

            NTContextModel model = new NTContextModel()
            {
                UserId = claims.First(c => c.Type == ExtJwtClaimTypes.sub).Value,
                UserName = claims.First(c => c.Type == ExtJwtClaimTypes.given_name).Value,
                UserEmail = claims.First(c => c.Type == ExtJwtClaimTypes.email).Value,
                IdentityUrl = claims.First(c => c.Type == ExtJwtClaimTypes.iss).Value,
                IdentityUserToken = context.GetTokenAsync("access_token").Result,
            };

            NTContext.Context = model;
        }
    }
}
