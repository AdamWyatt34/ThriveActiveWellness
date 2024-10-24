using ThriveActiveWellness.Common.Application.EventBus;
using ThriveActiveWellness.Modules.Users.IntegrationEvents;

namespace ThriveActiveWellness.Modules.Notifications.Presentation.PARQ;

internal sealed class UserParQCompletedIntegrationEventHandler : IntegrationEventHandler<UserParQCompletedIntegrationEvent>
{
    public override Task Handle(UserParQCompletedIntegrationEvent integrationEvent, CancellationToken cancellationToken = default)
    {
        // Need to create command to generate PDF
        // PDF Generation should trigger an email to the user to sign the PARQ using Syncfusion
        // Email should contain a link to the Syncfusion document
        throw new NotImplementedException();
    }
}
