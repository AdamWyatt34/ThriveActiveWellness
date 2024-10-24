namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public record ExerciseMuscleGroupId(Guid Value)
{
    public static ExerciseMuscleGroupId New() => new(Guid.NewGuid());
}
