using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations.AspNetIdentity
{
    public class EmailConfirmDataProtectorTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
    {
        public EmailConfirmDataProtectorTokenProvider(
            IDataProtectionProvider dataProtectionProvider,
            IOptions<EmailConfirmProtectorTokenProviderOptions> options
        ) : base(dataProtectionProvider, options)
        {
        }
    }
}
