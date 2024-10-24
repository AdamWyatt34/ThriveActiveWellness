using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;

public sealed record RegisterUserCommand(
    string Email,
    string FirstName,
    string LastName,
    string IdentityId)
    : ICommand<Guid>;
