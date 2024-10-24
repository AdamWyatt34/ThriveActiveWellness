using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Clients.CreateClient;

public sealed record CreateClientCommand(Guid ClientId, string Email, string FirstName, string LastName)
    : ICommand;
