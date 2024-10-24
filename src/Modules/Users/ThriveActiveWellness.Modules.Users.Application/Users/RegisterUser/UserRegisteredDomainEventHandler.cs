using MediatR;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Users.GetUser;
using ThriveActiveWellness.Modules.Users.Domain.Users;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;

internal sealed class UserRegisteredDomainEventHandler(ISender sender, IEventBus bus)
    : DomainEventHandler<UserRegisteredDomainEvent>
{
    public override async Task Handle(
        UserRegisteredDomainEvent domainEvent,
        CancellationToken cancellationToken = default)
    {
        Result<UserResponse> result = await sender.Send(
            new GetUserQuery(domainEvent.UserId),
            cancellationToken);

        if (result.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(GetUserQuery), result.Error);
        }

        await bus.PublishAsync(
            new UserRegisteredIntegrationEvent(
                domainEvent.Id,
                domainEvent.OccurredOnUtc,
                result.Value.Id,
                result.Value.Email,
                result.Value.FirstName,
                result.Value.LastName),
            cancellationToken);
    }
}
