using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Domain.Shared;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Domain.Notifications;

public class Notification : Entity
{
    public Guid Id { get; private set; }
    public NotificationType Type { get; private set; }
    public string TemplateId { get; private set; }
    public Guid UserId { get; set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? SentAt { get; private set; }
    
    private Notification()
    {
    }
    
    public static Notification Create(Guid id, NotificationType type, string templateId, Guid userId)
    {
        var notification = new Notification
        {
            Id = id,
            Type = type,
            TemplateId = templateId,
            CreatedAt = DateTime.UtcNow,
            UserId = userId
        };

        return notification;
    }

    public static Notification NewUser(Guid userId)
    {
        Notification notification = Create(
            Guid.NewGuid(), 
            NotificationType.Email,
            EmailTemplateConstants.WelcomeTemplateId,
            userId);
        
        notification.Raise(new UserCreatedDomainEvent(notification.Id));
        
        return notification;
    }
    
    public void MarkAsSent(DateTime sentAt)
    {
        SentAt = sentAt;
    }
}
