using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.SaveResponse;

public record SaveResponseCommand(
    Guid UserId,
    int QuestionId,
    string Answer) : ICommand;
