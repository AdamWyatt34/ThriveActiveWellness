using Shouldly;
using ThriveActiveWellness.Common.Application.Authorization;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Users.GetUserPermissions;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Users;

public class GetUserPermissionsTest : BaseIntegrationTest
{
    public GetUserPermissionsTest(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task Should_ReturnPermissions_WhenUserExists()
    {
        // Arrange
        string identityId = Faker.Random.Guid().ToString();
        
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            identityId
        ));
        
        Guid userId = result.Value;
        
        // Act
        Result<PermissionsResponse> userResult = await Sender.Send(new GetUserPermissionsQuery(identityId));
        
        // Assert
        userResult.IsSuccess.ShouldBeTrue();
        userResult.Value.ShouldNotBeNull();
        userResult.Value.UserId.ShouldBe(userId);
    }
}
