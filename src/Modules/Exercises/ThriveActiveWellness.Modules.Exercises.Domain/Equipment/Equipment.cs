using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

public class Equipment : Entity
{
    public EquipmentId Id { get; private set; }
    public string Name { get; private set; }
    public bool IsActive { get; private set; }
    public EquipmentTableId TableId { get; private set; }
    
    private Equipment()
    {
    }

    private Equipment(EquipmentId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        IsActive = true;
    }
    
    public static Equipment Create(EquipmentId id, string name)
    {
        return new Equipment(id, name);
    }
    
    public void Update(string name)
    {
        Name = name;
    }

    public void Delete()
    {
        IsActive = false;
    }
}
