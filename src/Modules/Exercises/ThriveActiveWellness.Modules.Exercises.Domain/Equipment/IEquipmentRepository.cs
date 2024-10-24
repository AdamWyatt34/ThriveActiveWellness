namespace ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

public interface IEquipmentRepository
{
    Task<Equipment> GetByIdAsync(EquipmentId id);
    void Add(Equipment equipment);
    void Update(Equipment equipment);
    void Delete(Equipment equipment);
}
