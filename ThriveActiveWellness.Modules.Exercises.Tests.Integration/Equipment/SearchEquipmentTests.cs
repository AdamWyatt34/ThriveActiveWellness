using Shouldly;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.SearchEquipment;
using ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Equipment;

public class SearchEquipmentTests : BaseIntegrationTest
{
    public SearchEquipmentTests(IntegrationTestWebAppFactory factory) : base(factory)
    {
    }

    [Fact]
    public async Task Handle_ShouldReturnEquipment_WhenValidRequest()
    {
        // Arrange
        const string search = "equipment";
        const int page = 1;
        const int pageSize = 10;
        
        await CreateEquipment(search);
        
        // Act
        Result<SearchEquipmentResponse> result = await Sender.Send(new SearchEquipmentQuery(search, page, pageSize));
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Equipment.Count.ShouldBeGreaterThan(0);
    }
    
    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoEquipmentFound()
    {
        // Arrange
        const int page = 1;
        const int pageSize = 10;
        
        // Act
        Result<SearchEquipmentResponse> result = await Sender.Send(new SearchEquipmentQuery(Guid.NewGuid().ToString(), page, pageSize));
    
        // Assert
        result.IsSuccess.ShouldBeTrue();
        result.Value.Equipment.Count.ShouldBe(0);
    }
    
    private async Task<Guid> CreateEquipment(string name)
    {
        Result<Guid> result = await Sender.Send(new CreateEquipmentCommand(name));
        return result.Value;
    }
}
