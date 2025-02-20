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
}
