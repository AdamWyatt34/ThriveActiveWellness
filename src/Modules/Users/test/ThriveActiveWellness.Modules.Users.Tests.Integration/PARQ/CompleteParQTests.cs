using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.PARQ.CompleteParQ;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.PARQ;

public class CompleteParQTests : BaseIntegrationTest
{
    public CompleteParQTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenParQIsCompleted()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Random.Guid().ToString()
        ));

        Guid userId = result.Value;

        // Act
        Result saveResult = await Sender.Send(new CompleteParQCommand(userId));

        // Assert
        saveResult.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task Should_ReturnError_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        Result saveResult = await Sender.Send(new CompleteParQCommand(userId));

        // Assert
        saveResult.IsFailure.ShouldBeTrue();
        saveResult.Error.Type.ShouldBe(ErrorType.NotFound);
    }
}
