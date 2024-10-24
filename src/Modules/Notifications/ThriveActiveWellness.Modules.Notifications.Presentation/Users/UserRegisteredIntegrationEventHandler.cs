using MediatR;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Application.Users.CreateUser;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Notifications.Presentation.Users;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender) 
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateUserCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(CreateUserCommand), result.Error);
        }
    }   
}
