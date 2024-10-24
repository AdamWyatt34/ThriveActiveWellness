using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Notifications;

internal sealed class NotificationRepository(NotificationsDbContext context) : INotificationRepository
{
    public async Task<Notification?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Notifications.SingleOrDefaultAsync(n => n.Id == id, cancellationToken);
    }

    public void Insert(Notification notification)
    {
        context.Notifications.Add(notification);
    }

    public void Update(Notification notification)
    {
        context.Notifications.Update(notification);
    }
}
