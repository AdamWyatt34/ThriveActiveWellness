namespace ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

public interface IEmailService
{
    Task SendWelcomeEmailAsync(string email, string firstName, string lastName, CancellationToken cancellationToken = new());
}
