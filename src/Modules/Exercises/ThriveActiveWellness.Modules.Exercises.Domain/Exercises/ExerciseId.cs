namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public record ExerciseId(Guid Value)
{
    public static ExerciseId New() => new(Guid.NewGuid());
}
