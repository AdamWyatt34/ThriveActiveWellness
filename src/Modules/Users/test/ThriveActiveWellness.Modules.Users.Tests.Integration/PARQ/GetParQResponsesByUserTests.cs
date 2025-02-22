using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQResponsesByUser;
using ThriveActiveWellness.Modules.Users.Application.PARQ.SaveResponse;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.PARQ;

public class GetParQResponsesByUserTests : BaseIntegrationTest
{
    public GetParQResponsesByUserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Should_ReturnResponses_WhenUserAndResponsesExist()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Random.Guid().ToString()
        ));

        Guid userId = result.Value;
        
        await Sender.Send(new SaveResponseCommand(userId, 1, "Yes"));

        // Act
        Result<IEnumerable<ParqResponseRecord>> responses = await Sender.Send(new GetParQResponseByUserQuery(userId));

        // Assert
        responses.IsSuccess.ShouldBeTrue();
        responses.Value.ShouldNotBeEmpty();
    }

    [Fact]
    public async Task Should_ReturnEmptyList_WhenUserDoesNotExist()
    {
        // Arrange
        var userId = Guid.NewGuid();

        // Act
        Result<IEnumerable<ParqResponseRecord>> responses = await Sender.Send(new GetParQResponseByUserQuery(userId));

        // Assert
        responses.IsSuccess.ShouldBeTrue();
        responses.Value.ShouldBeEmpty();
    }
}
