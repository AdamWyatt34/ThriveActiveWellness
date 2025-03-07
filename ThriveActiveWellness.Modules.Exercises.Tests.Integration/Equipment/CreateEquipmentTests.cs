using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;
using ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Equipment;

public class CreateEquipmentTests : BaseIntegrationTest
{
    public CreateEquipmentTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldCreateEquipmentAndReturnId_WhenValidRequest()
    {
        // Act
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(Faker.Commerce.ProductName()));

        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldBeOfType<Guid>();
        result.Value.ShouldNotBe(Guid.Empty);
    }
    
    [Fact]
    public async Task Handle_ShouldNotCreateEquipmentAndReturnError_WhenNameIsInvalid()
    {
        // Act
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(string.Empty));

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(null);
    }
    
    [Fact]
    public async Task Handle_ShouldNotCreateEquipmentAndReturnError_WhenNameIsTooLong()
    {
        // Act
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(Faker.Lorem.Sentence(100)));

        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(null);
        result.Error.Type.ShouldBe(ErrorType.Validation);
    }
}
