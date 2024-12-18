using Aspire.Hosting;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration;

public class IntegrationTest1
{
    // Instructions:
    // 1. Add a project reference to the target AppHost project, e.g.:
    //
    //    <ItemGroup>
    //        <ProjectReference Include="../MyAspireApp.AppHost/MyAspireApp.AppHost.csproj" />
    //    </ItemGroup>
    //
    // 2. Uncomment the following example test and update 'Projects.MyAspireApp_AppHost' to match your AppHost project:
    //
    [Fact]
    public async Task GetWebResourceRootReturnsOkStatusCode()
    {
        // Arrange
        IDistributedApplicationTestingBuilder appHost = await DistributedApplicationTestingBuilder.CreateAsync<Projects.ThriveActiveWellness_AppHost>();
        appHost.Services.ConfigureHttpClientDefaults(clientBuilder =>
        {
            clientBuilder.AddStandardResilienceHandler();
        });
        // To output logs to the xUnit.net ITestOutputHelper, consider adding a package from https://www.nuget.org/packages?q=xunit+logging
    
        await using DistributedApplication app = await appHost.BuildAsync();
        ResourceNotificationService resourceNotificationService = app.Services.GetRequiredService<ResourceNotificationService>();
        await app.StartAsync();

        // Act
        using HttpClient httpClient = app.CreateHttpClient("webfrontend");
        await resourceNotificationService.WaitForResourceAsync("webfrontend", KnownResourceStates.Running).WaitAsync(TimeSpan.FromSeconds(30));
        
        Assert.True(true, "The test is a placeholder and should be replaced with actual test code.");
    }
}
