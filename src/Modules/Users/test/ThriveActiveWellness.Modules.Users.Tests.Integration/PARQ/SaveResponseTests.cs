using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.PARQ.SaveResponse;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.PARQ;

public class SaveResponseTests : BaseIntegrationTest
{
    public SaveResponseTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenResponseIsSaved()
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
        Result saveResult = await Sender.Send(new SaveResponseCommand(userId, 1, "Yes"));

        // Assert
        saveResult.IsSuccess.ShouldBeTrue();
    }
}
