using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using ThriveActiveWellness.UI;
using ThriveActiveWellness.UI.Extensions;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



builder.Services.AddMsalAuthentication(options =>

{
    builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
    options.ProviderOptions.DefaultAccessTokenScopes.Add("https://graph.microsoft.com/User.Read");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("openid");
    options.ProviderOptions.DefaultAccessTokenScopes.Add("offline_access");
    options.ProviderOptions.AdditionalScopesToConsent.Add(builder.Configuration["DownstreamApi:ClientId"] ?? throw new InvalidOperationException("Missing DownstreamApi:ClientId configuration value"));
    options.ProviderOptions.LoginMode = "Redirect";
});

builder.Services.AddClients(builder.Configuration)
    .AddApiClients(builder.Configuration)
    .AddBlazoredSessionStorage();

builder.Services.AddMudServices();

await builder.Build().RunAsync();
