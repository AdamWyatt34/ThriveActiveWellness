using Bogus;
using MediatR;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly UsersDbContext DbContext;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<UsersDbContext>();
    }
    
    public void Dispose()
    {
        _scope.Dispose();
    }
}
