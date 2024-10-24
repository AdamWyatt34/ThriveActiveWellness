using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;

public sealed record CreateUserCommand(Guid UserId, string Email, string FirstName, string LastName)
    : ICommand;
