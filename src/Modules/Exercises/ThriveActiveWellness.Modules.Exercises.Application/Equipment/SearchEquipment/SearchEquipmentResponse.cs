using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.SearchEquipment;

public record SearchEquipmentResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<EquipmentResponse> Equipment
    );
