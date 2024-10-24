namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public record MuscleGroupId(Guid Value)
{
    public static MuscleGroupId New() => new(Guid.NewGuid());
}
