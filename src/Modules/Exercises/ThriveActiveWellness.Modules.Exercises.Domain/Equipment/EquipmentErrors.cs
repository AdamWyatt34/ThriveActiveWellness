using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

public static class EquipmentErrors
{
    public static readonly Error UniqueNameError = Error.Conflict(
        "Equipment.UniqueNameError",
        "Equipment name must be unique.");
    
    public static readonly Error NotFound = Error.NotFound(
        "Equipment.EquipmentNotFoundError",
        "Equipment not found.");
    
    public static readonly Error EquipmentInUseError = Error.Problem(
        "Equipment.EquipmentInUseError",
        "Equipment is in use.");
}
