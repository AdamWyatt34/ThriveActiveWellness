using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using ThriveActiveWellness.UI.Options;

namespace ThriveActiveWellness.UI.Extensions;

public class CustomAuthorizationMessageHandler : AuthorizationMessageHandler
{
    public CustomAuthorizationMessageHandler(
        IConfiguration configuration,
        IAccessTokenProvider provider,
        NavigationManager navigationManager)
        : base(provider, navigationManager)
    {
        DownstreamApiConfiguration downstreamApi = configuration.GetRequiredSection("DownstreamApi")
            .Get<DownstreamApiConfiguration>()!;
            
        ConfigureHandler(
            authorizedUrls: [downstreamApi.BaseUrl],
            scopes: downstreamApi.Scopes
        );
    }
}
