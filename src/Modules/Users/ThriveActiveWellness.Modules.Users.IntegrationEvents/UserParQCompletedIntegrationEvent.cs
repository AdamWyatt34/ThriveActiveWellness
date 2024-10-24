using ThriveActiveWellness.Common.Application.EventBus;

namespace ThriveActiveWellness.Modules.Users.IntegrationEvents;

public sealed class UserParQCompletedIntegrationEvent : IntegrationEvent
{
    public UserParQCompletedIntegrationEvent(Guid userId, List<ParQItemModel> parQItems) : base(Guid.NewGuid(), DateTime.UtcNow)
    {
        UserId = userId;
        ParQItems = parQItems;
    }

    public Guid UserId { get; init; }

    public List<ParQItemModel> ParQItems { get; init; }
}
