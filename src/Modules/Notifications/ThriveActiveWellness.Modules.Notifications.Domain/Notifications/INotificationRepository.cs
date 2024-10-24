namespace ThriveActiveWellness.Modules.Notifications.Domain.Notifications;

public interface INotificationRepository
{
    Task<Notification?> GetAsync(Guid id, CancellationToken cancellationToken = default);
    void Insert(Notification notification);
    void Update(Notification notification);
}
