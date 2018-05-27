using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Infrastructure.Messaging
{
    public interface IEmailSender
    {
        Task SendEmailAsync(
            SmtpOptions smtpOptions,
            string to,
            string from,
            string subject,
            string plainTextMessage,
            string htmlMessage,
            string replyTo = null,
            Importance importance = Importance.Normal);

        Task SendMultipleEmailAsync(
            SmtpOptions smtpOptions,
            string toCsv,
            string from,
            string subject,
            string plainTextMessage,
            string htmlMessage,
            string replyTo = null,
            Importance importance = Importance.Normal);
    }
}
