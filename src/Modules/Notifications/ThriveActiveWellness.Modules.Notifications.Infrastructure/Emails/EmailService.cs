using SendGrid;
using SendGrid.Helpers.Mail;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Application.Constants;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Emails.Models;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Emails;

public class EmailService(ISendGridClient sendGridClient) : IEmailService
{

    public async Task SendWelcomeEmailAsync(string email, string firstName, string lastName, CancellationToken cancellationToken = new())
    {
        SendGridMessage? message = MailHelper.CreateSingleTemplateEmail(
            EmailConstants.FromAddress,
            new EmailAddress(email, $"{firstName} {lastName}"),
            EmailTemplateConstants.WelcomeTemplateId,
            new SendWelcomeEmailModel(firstName, lastName)
        );
        
        await sendGridClient.SendEmailAsync(message, cancellationToken);
    }
}
