using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public class MuscleGroup : Entity
{
    public MuscleGroupId Id { get; private set; }
    public string Name { get; private set; }
    public MuscleGroupTableId TableId { get; private set; }

    private MuscleGroup()
    {
    }
    
    private MuscleGroup(MuscleGroupId id, string name)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
    }
    
    public static MuscleGroup Create(string name)
    {
        return new MuscleGroup(MuscleGroupId.New(), name);
    }
    
    public static MuscleGroup Create(MuscleGroupId id, string name)
    {
        return new MuscleGroup(id, name);
    }
}
