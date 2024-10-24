using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Equipment.SearchEquipment;

public sealed record SearchEquipmentQuery(
    string? Search,
    int Page,
    int PageSize
    ) : IQuery<SearchEquipmentResponse>;
