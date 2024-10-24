using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Domain.Shared;

namespace ThriveActiveWellness.Modules.Notifications.Domain.NotificationPreference;

public class NotificationPreference : Entity
{
    public Guid Id { get; private set; }
    public int UserTableId { get; private set; }
    public NotificationType Type { get; private set; }
    public bool Enabled { get; private set; }

    private NotificationPreference()
    {
    }

    public static NotificationPreference Create(Guid id, int userId, NotificationType type, bool enabled)
    {
        var notificationPreference = new NotificationPreference
        {
            Id = id,
            UserTableId = userId,
            Type = type,
            Enabled = enabled
        };

        return notificationPreference;
    }

    public void Update(bool enabled)
    {
        Enabled = enabled;
    }
}
