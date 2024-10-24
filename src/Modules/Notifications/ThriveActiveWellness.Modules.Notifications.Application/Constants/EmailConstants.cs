using SendGrid.Helpers.Mail;

namespace ThriveActiveWellness.Modules.Notifications.Application.Constants;


public static class EmailConstants
{
    public static EmailAddress FromAddress => new("adam.wyatt@thriveactivewellness.com", "Adam Wyatt");
    public static EmailAddress SupportAddress => new("support@thriveactivewellness.com", "Thrive Active Wellness Support");
}
