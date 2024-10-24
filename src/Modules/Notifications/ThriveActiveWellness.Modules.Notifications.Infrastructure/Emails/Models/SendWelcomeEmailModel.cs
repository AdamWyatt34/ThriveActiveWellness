using Newtonsoft.Json;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Emails.Models;

internal sealed class SendWelcomeEmailModel(string firstName, string lastName)
{
    [JsonProperty("first_name")]
    public string FirstName { get; } = firstName;

    [JsonProperty("last_name")]
    public string LastName { get; } = lastName;
}
