using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.UpdateEquipment;
using ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Equipment;

public class UpdateEquipmentTests : BaseIntegrationTest
{
    public UpdateEquipmentTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldUpdateEquipmentAndReturnSuccess_WhenValidRequest()
    {
        // Arrange
        Guid equipmentId = await CreateEquipment();
        string newName = Faker.Commerce.ProductName();
        
        // Act
        Result result = await Sender.Send(new UpdateEquipmentCommand(equipmentId, newName));
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task Handle_ShouldNotUpdateEquipmentAndReturnError_WhenNameIsInvalid()
    {
        // Arrange
        Guid equipmentId = await CreateEquipment();
        
        // Act
        Result result = await Sender.Send(new UpdateEquipmentCommand(equipmentId, string.Empty));
    
        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(null);
    }
    
    [Fact]
    public async Task Handle_ShouldNotUpdateEquipmentAndReturnError_WhenNameIsTooLong()
    {
        // Arrange
        Guid equipmentId = await CreateEquipment();
        
        // Act
        Result result = await Sender.Send(new UpdateEquipmentCommand(equipmentId, Faker.Lorem.Sentence(100)));
    
        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(null);
        result.Error.Type.ShouldBe(ErrorType.Validation);
    }
    
    private async Task<Guid> CreateEquipment()
    {
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(Faker.Commerce.ProductName()));
        return result.Value;
    }
}
