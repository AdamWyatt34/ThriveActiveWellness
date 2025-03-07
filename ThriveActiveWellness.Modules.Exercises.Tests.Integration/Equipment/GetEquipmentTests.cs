using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;
using ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Equipment;

public class GetEquipmentTests : BaseIntegrationTest
{
    public GetEquipmentTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldReturnEquipment_WhenValidRequest()
    {
        // Arrange
        Guid equipmentId = await CreateEquipment();
        
        // Act
        Result<EquipmentResponse> result = await Sender.Send(new GetEquipmentQuery(equipmentId));
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBe(null);
        result.Value.EquipmentId.ShouldBe(equipmentId);
    }
    
    [Fact]
    public async Task Handle_ShouldNotReturnEquipment_WhenEquipmentDoesNotExist()
    {
        // Arrange
        var equipmentId = Guid.NewGuid();
        
        // Act
        Result<EquipmentResponse> result = await Sender.Send(new GetEquipmentQuery(equipmentId));
    
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
