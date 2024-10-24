using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;

public sealed record SendWelcomeEmailCommand(Guid NotificationId) : ICommand;
