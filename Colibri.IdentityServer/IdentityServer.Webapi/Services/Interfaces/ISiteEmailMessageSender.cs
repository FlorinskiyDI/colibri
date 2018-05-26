using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Services.Interfaces
{
    public interface ISiteEmailMessageSender
    {
        Task SendAccountConfirmationEmailAsync(
            object siteSettings,
            string toAddress,
            string subject,
            string confirmationUrl);

        //Task SendSecurityCodeEmailAsync(
        //    ISiteContext siteSettings,
        //    string toAddress,
        //    string subject,
        //    string securityCode);

        Task SendPasswordResetEmailAsync(
            //ISiteContext siteSettings,
            object siteSettings,
            string toAddress,
            string subject,
            string resetUrl);

        //Task AccountPendingApprovalAdminNotification(
        //    ISiteContext siteSettings,
        //    ISiteUser user);

        //Task SendAccountApprovalNotificationAsync(
        //    ISiteContext siteSettings,
        //    string toAddress,
        //    string subject,
        //    string loginUrl);
    }
}
