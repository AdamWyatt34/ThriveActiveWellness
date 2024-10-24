using ThriveActiveWellness.Common.Application.Clock;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;

public class SendWelcomeEmailCommandHandler(
    INotificationRepository notificationRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork,
    IEmailService emailService,
    IDateTimeProvider dateTimeProvider) 
    : ICommandHandler<SendWelcomeEmailCommand>
{
    public async Task<Result> Handle(SendWelcomeEmailCommand request, CancellationToken cancellationToken)
    { 
        Notification? notification = await notificationRepository.GetAsync(request.NotificationId, cancellationToken);
        
        if (notification is null)
        {
            return Result.Failure(NotificationErrors.NotFound(request.NotificationId));
        }
        
        User? user = await userRepository.GetAsync(notification.UserId, cancellationToken);
        
        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(notification.UserId));
        }
        
        await emailService.SendWelcomeEmailAsync(
            user.Email,
            user.FirstName,
            user.LastName,
            cancellationToken);
        
        notification.MarkAsSent(dateTimeProvider.UtcNow);
        
        notificationRepository.Update(notification);
        
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
