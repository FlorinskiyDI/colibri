using IdentityModel;
using IdentityServer.Webapi.Data;
using IdentityServer.Webapi.Repositories.Interfaces;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.ResponseHandling;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
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

        private readonly IAppUserRoleRepository _appUserRoleRepository;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AccountChooserResponseGenerator(
            RoleManager<ApplicationRole> roleManager,
        IAppUserRoleRepository appUserRoleRepository,
            ISystemClock clock,
            ILogger<AuthorizeInteractionResponseGenerator> logger,
            IConsentService consent, IProfileService profile)
            : base(clock, logger, consent, profile)
        {
            _roleManager = roleManager;
            _appUserRoleRepository = appUserRoleRepository;
        }

        public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        {
            try
            {
                var tenant = ValidatedAuthorizeRequestExtensions.GetTenant(request);
                var subject = request.Subject.GetSubjectId();
                var name = request.Subject.GetDisplayName();
                var claims = request.Subject.Claims.ToList();

                if (tenant != null)
                {
                    claims.Add(new Claim("tenant", tenant));
                    var roleList = await this._appUserRoleRepository.GetRolesByGroupAsync(new Guid(subject), new Guid(tenant));
                    foreach (var role in roleList)
                    {
                        var roleClaims = await _roleManager.GetClaimsAsync(role);
                        claims.Add(new Claim("role", role.Name));
                        claims.AddRange(roleClaims);
                    }
                }

                var newPrincipal = IdentityServerPrincipal.Create(subject, name, claims.ToArray());
                request.Subject = newPrincipal;

                return new InteractionResponse();
            }
            catch (Exception)
            {

                return await base.ProcessInteractionAsync(request, consent);
            }

            //return await base.ProcessInteractionAsync(request, consent);
        }

        //public override async Task<InteractionResponse> ProcessInteractionAsync(ValidatedAuthorizeRequest request, ConsentResponse consent = null)
        //{
        //    Logger.LogTrace("ProcessInteractionAsync");

        //    if (consent != null && consent.Granted == false && request.Subject.IsAuthenticated() == false)
        //    {
        //        // special case when anonymous user has issued a deny prior to authenticating
        //        Logger.LogInformation("Error: User denied consent");
        //        return new InteractionResponse
        //        {
        //            Error = OidcConstants.AuthorizeErrors.AccessDenied
        //        };
        //    }

        //    var result = await ProcessLoginAsync(request);
        //    if (result.IsLogin || result.IsError)
        //    {
        //        return result;
        //    }

        //    result = await ProcessConsentAsync(request, consent);

        //    return result;
        //}

    }
}
