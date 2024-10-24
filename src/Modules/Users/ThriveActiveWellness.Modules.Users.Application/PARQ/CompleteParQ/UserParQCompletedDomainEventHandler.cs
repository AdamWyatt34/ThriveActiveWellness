using MediatR;
using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQResponsesByUser;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.CompleteParQ;

internal sealed class UserParQCompletedDomainEventHandler(ISender sender, IEventBus eventBus) : DomainEventHandler<UserParQCompletedDomainEvent>
{
    public override async Task Handle(
        UserParQCompletedDomainEvent notification,
        CancellationToken cancellationToken = default)
    {
        Result<IEnumerable<ParqResponseRecord>> userParQ = await sender.Send(new GetParQResponseByUserQuery(notification.UserId), cancellationToken);
        
        if (userParQ.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(GetParQResponseByUserQuery), userParQ.Error);
        }
        
        await eventBus.PublishAsync(
            new UserParQCompletedIntegrationEvent(
                notification.UserId,
                userParQ.Value.Select(p => new ParQItemModel
                {
                    Id = p.Id,
                    Question = p.Question,
                    Answer = p.Answer
                }).ToList()),
            cancellationToken);
    }
}
