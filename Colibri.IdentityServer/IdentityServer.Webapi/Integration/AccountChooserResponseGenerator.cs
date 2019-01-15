using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Integration
{
    public class AccountChooserResponseGenerator : AuthorizeInteractionResponseGenerator
    {
        public AccountChooserResponseGenerator(ISystemClock clock,
            ILogger<AuthorizeInteractionResponseGenerator> logger,
            IConsentService consent, IProfileService profile)
            : base(clock, logger, consent, profile)
        {
        }

        public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
       //     var tenant = ValidatedAuthorizeRequestExtensions.GetTenant(request);

       //         var claims = new[]
       //         {
       //   new Claim(JwtClaimTypes.Name, "Fred Blogs"),
       //   new Claim(JwtClaimTypes.FamilyName, "Blogs"),
       //   new Claim(JwtClaimTypes.GivenName, "Fred"),
       //   new Claim("tenant", tenant),
       //};

       //         var newPrincipal = IdentityServerPrincipal.Create("fred.blogs", "Fred Blogs", claims);
       //         request.Subject = newPrincipal;

                //return new InteractionResponse();


            return await base.ProcessInteractionAsync(request, consent);
        }
    }
}
