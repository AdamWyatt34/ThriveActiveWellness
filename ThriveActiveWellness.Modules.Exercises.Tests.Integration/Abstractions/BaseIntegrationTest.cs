using Bogus;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

[Collection(nameof(IntegrationTestCollection))]
public abstract class BaseIntegrationTest : IDisposable
{
    protected static readonly Faker Faker = new();
    private readonly IServiceScope _scope;
    protected readonly ISender Sender;
    protected readonly ExercisesDbContext DbContext;
    protected readonly IUnitOfWork UnitOfWork;
    
    protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();
        Sender = _scope.ServiceProvider.GetRequiredService<ISender>();
        DbContext = _scope.ServiceProvider.GetRequiredService<ExercisesDbContext>();
        UnitOfWork = _scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    }
    
    public void Dispose()
    {
        _scope.Dispose();
    }
}
