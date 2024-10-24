using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.PARQ;

public sealed class UserParQCompletedDomainEvent(Guid userId) : DomainEvent
{
    public Guid UserId { get; init; } = userId;
}
