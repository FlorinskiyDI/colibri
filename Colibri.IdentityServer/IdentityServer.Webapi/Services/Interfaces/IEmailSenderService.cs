using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface IEmailSenderService
    {
        Task SendAccountConfirmationEmailAsync(object siteSettings, string toAddress, string subject, string confirmationUrl);
        Task SendPasswordResetEmailAsync(object siteSettings, string toAddress, string subject, string resetUrl);
    }
}
