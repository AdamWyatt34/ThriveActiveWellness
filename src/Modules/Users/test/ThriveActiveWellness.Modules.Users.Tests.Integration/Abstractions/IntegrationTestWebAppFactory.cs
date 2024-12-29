using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.PostgreSql;
using Testcontainers.Redis;
using ThriveActiveWellness.Common.Infrastructure.Constants;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    
    private readonly PostgreSqlContainer _dbContainer = new PostgreSqlBuilder()
        .WithImage("postgres:latest")
        .WithDatabase("ThriveActiveWellness")
        .WithUsername("postgres")
        .WithPassword("postgres")
        .Build();

    private readonly RedisContainer _redisContainer = new RedisBuilder()
        .WithImage("redis:latest")
        .Build();
    
    public async Task InitializeAsync()
    {
        await _dbContainer.StartAsync();
        await _redisContainer.StartAsync();
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        Environment.SetEnvironmentVariable($"ConnectionStrings:{ServiceNames.Database}", _dbContainer.GetConnectionString());
        Environment.SetEnvironmentVariable($"ConnectionStrings:{ServiceNames.Queue}", _redisContainer.GetConnectionString());

        builder.ConfigureTestServices(services =>
        {
            // Mock authentication for integration tests
            services.AddAuthentication("Mock")
                .AddScheme<AuthenticationSchemeOptions, TestAuthenticationHandler>("Mock", _ => { });

            services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddAuthenticationSchemes("Mock")
                    .Build());
        });
    }

    public new async Task DisposeAsync()
    {
        await _dbContainer.DisposeAsync();
        await _redisContainer.DisposeAsync();
    }
}
