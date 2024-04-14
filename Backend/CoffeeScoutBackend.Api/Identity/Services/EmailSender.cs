using CoffeeScoutBackend.Api.Config;
using MailerSendNetCore.Common.Interfaces;
using MailerSendNetCore.Emails.Dtos;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;

namespace CoffeeScoutBackend.Api.Identity.Services;

public class EmailSender(
    IOptions<MailerSendSettings> mailerSendSettings,
    IMailerSendEmailClient mailerSendEmailClient
) : IEmailSender
{
    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var response = await mailerSendEmailClient.SendEmailAsync(new MailerSendEmailParameters
        {
            From = new MailerSendEmailRecipient(mailerSendSettings.Value.SenderEmail, "CoffeeScout"),
            To = new List<MailerSendEmailRecipient>()
            {
                new(email, string.Empty)
            },
            Cc = ArraySegment<MailerSendEmailRecipient>.Empty,
            Bcc = ArraySegment<MailerSendEmailRecipient>.Empty,
            Subject = subject,
            Html = htmlMessage,
        });

        if (response.Errors is not null && response.Errors.Count != 0)
        {
            var errors = string.Join(", ", response.Errors.Select(e => e.Value));
            throw new InvalidOperationException($"Email could not be sent, errors: {errors}");
        }
    }
}