using MediatR;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Clients.CreateClient;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Clients;

internal sealed class UserRegisteredIntegrationEventHandler(ISender sender) 
    : IntegrationEventHandler<UserRegisteredIntegrationEvent>
{
    public override async Task Handle(
        UserRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default)
    {
        Result result = await sender.Send(
            new CreateClientCommand(
                integrationEvent.UserId,
                integrationEvent.Email,
                integrationEvent.FirstName,
                integrationEvent.LastName),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(CreateClientCommand), result.Error);
        }
    }   
}
