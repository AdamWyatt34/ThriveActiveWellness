using Microsoft.EntityFrameworkCore;
using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Domain.Users;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Users;

public class RegisterUserCommandHandlerTests : BaseIntegrationTest
{
    public RegisterUserCommandHandlerTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldRegisterUserAndReturnId_WhenValidRequest()
    {
        // Act
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Random.Guid().ToString()
        ));
        
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeOfType<Guid>();
        result.Value.ShouldNotBe(Guid.Empty);
    }

    [Fact]
    public async Task ThisisATest()
    {
        var user = User.Create(
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Internet.Email(),
            Faker.Random.Guid().ToString(),
            Role.Client);
        
        DbContext.Users.Add(user);
        
        await UnitOfWork.SaveChangesAsync();
        
        // Verify user is saved correctly
        User? updatedUser = await DbContext.Users.FindAsync(user.TableId);
        
        updatedUser.ShouldNotBeNull();
    }
}
