using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.Users.UpdateUser;

public sealed record UpdateUserCommand(Guid UserId, string FirstName, string LastName, string Email) : ICommand;
