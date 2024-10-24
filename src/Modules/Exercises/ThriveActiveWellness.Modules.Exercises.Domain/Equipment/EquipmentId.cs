namespace ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

public record EquipmentId(Guid Value)
{
    public static EquipmentId New() => new(Guid.NewGuid());
    // empty constructor for integration tests
    public EquipmentId() : this(Guid.Empty) { }
}
