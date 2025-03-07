using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.DeleteEquipment;
using ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Equipment;

public class DeleteEquipmentTests : BaseIntegrationTest
{
    public DeleteEquipmentTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldDeleteEquipmentAndReturnSuccess_WhenValidRequest()
    {
        // Arrange
        Guid equipmentId = await CreateEquipment();
        
        // Act
        Result result = await Sender.Send(new DeleteEquipmentCommand(equipmentId));
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
    }
    
    [Fact]
    public async Task Handle_ShouldNotDeleteEquipmentAndReturnError_WhenEquipmentDoesNotExist()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        
        // Act
        Result result = await Sender.Send(new DeleteEquipmentCommand(equipmentId));
    
        // Assert
        result.IsSuccess.ShouldBeFalse();
        result.Error.ShouldNotBe(null);
        result.Error.Type.ShouldBe(ErrorType.NotFound);
    }
    
    private async Task<Guid> CreateEquipment()
    {
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(Faker.Commerce.ProductName()));
        return result.Value;
    }
}
