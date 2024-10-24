using MediatR;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;

public class UserCreatedDomainEventHandler(ISender sender) : DomainEventHandler<UserCreatedDomainEvent>
{
    public override async Task Handle(UserCreatedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(new SendWelcomeEmailCommand(domainEvent.NotificationId), cancellationToken);
        
        if (result.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(SendWelcomeEmailCommand), result.Error);
        }
    }
}
