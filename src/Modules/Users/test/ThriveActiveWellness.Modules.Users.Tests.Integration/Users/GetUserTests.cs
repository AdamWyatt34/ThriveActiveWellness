using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Users.GetUser;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Users;

public class GetUserTests : BaseIntegrationTest
{
    public GetUserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Should_ReturnUser_WhenUserExists()
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
        Result<UserResponse> userResult = await Sender.Send(new GetUserQuery(userId));
        
        // Assert
        userResult.IsSuccess.ShouldBeTrue();
        userResult.Value.ShouldNotBeNull();
        userResult.Value.Id.ShouldBe(userId);
    }
}
