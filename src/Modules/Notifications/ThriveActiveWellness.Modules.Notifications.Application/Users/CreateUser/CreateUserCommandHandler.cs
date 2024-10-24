using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;

public class CreateUserCommandHandler(IUserRepository userRepository, INotificationRepository notificationRepository, IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateUserCommand>
{
    public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(request.UserId,  request.FirstName, request.LastName, request.Email);

        var notification = Notification.NewUser(user.Id);
        
        userRepository.Insert(user);
        notificationRepository.Insert(notification);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
