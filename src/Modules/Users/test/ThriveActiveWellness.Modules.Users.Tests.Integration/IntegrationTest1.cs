using Aspire.Hosting;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration;

public class IntegrationTest1 : BaseIntegrationTest
{
    public IntegrationTest1(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public void GetWebResourceRootReturnsOkStatusCode()
    {
        Assert.True(true, "The test is a placeholder and should be replaced with actual test code.");
    }
}
