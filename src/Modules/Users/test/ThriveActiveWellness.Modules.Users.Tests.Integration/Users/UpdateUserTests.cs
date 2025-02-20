using Microsoft.EntityFrameworkCore;
using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;
using ThriveActiveWellness.Modules.Users.Application.Users.UpdateUser;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Users.Tests.Integration.Users;

public class UpdateUserTests : BaseIntegrationTest
{
    public UpdateUserTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    public static readonly TheoryData<UpdateUserCommand> InvalidCommands =
    [
        new(Guid.Empty, Faker.Name.FirstName(), Faker.Name.LastName(), Faker.Person.Email),
        new(Guid.NewGuid(), "", Faker.Name.LastName(), Faker.Person.Email),
        new(Guid.NewGuid(), Faker.Name.FirstName(), "", Faker.Person.Email)
    ];
    
    [Theory, MemberData(nameof(InvalidCommands))]
    public async Task Should_ReturnError_WhenCommandIsNotValid(UpdateUserCommand command)
    {
        // Act
        Result result = await Sender.Send(command);
        
        // Assert
        result.IsFailure.ShouldBeTrue();
        result.Error.Type.ShouldBe(ErrorType.Validation);
    }

    [Fact]
    public async Task Should_ReturnSuccess_WhenUserExists()
    {
        // Arrange
        Result<Guid> result = await Sender.Send(new RegisterUserCommand(
            Faker.Internet.Email(),
            Faker.Person.FirstName,
            Faker.Person.LastName,
            Faker.Random.Guid().ToString()
        ));
        
        Guid userId = result.Value;
        
        // Ensure changes are saved and detach any tracked entities
        await DbContext.SaveChangesAsync();
        DbContext.ChangeTracker.Clear();
    
        // Load user by Id with explicit column selection
        var user = await DbContext.Users
            .Where(u => u.Id == userId)
            .Select(u => new
            {
                u.Id,
                u.TableId,
                u.FirstName,
                u.LastName,
                u.Email
            })
            .FirstOrDefaultAsync();
    
        user.ShouldNotBeNull();
        
        // Act
        Result updateResult = await Sender.Send(
            new UpdateUserCommand(userId, Faker.Name.FirstName(), Faker.Name.LastName(), Faker.Person.Email)
        );
        
        updateResult.IsSuccess.ShouldBeTrue();
    }
}
