using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Notifications.Domain.Notifications;

public static class NotificationErrors
{
    public static Error NotFound(Guid notificationId) =>
        Error.NotFound("Notification.NotFound", $"The Notification with the identifier {notificationId} not found");
}
