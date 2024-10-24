namespace ThriveActiveWellness.Modules.Notifications.Application.Abstractions;

public interface ITokenService
{
    Task<string> GetAccessTokenAsync();
}
