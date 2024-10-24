using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Notifications.Domain.Users;

public sealed class UserCreatedDomainEvent(Guid notificationId) : DomainEvent
{
    public Guid NotificationId { get; init; } = notificationId;
}
