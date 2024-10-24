using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.CompleteParQ;

public record CompleteParQCommand(Guid UserId) : ICommand;
