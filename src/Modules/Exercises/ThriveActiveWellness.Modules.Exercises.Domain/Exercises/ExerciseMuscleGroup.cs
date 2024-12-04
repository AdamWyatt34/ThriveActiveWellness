using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public class ExerciseMuscleGroup : Entity
{
    public ExerciseMuscleGroupId Id { get; private set; }
    public ExerciseTableId ExerciseId { get; private set; }
    public MuscleGroupTableId MuscleGroupId { get; private set; }
    public MuscleGroupType MuscleGroupType { get; private set; }
    public ExerciseMuscleGroupTableId TableId { get; private set; }

    private ExerciseMuscleGroup()
    {
    }
    
    private ExerciseMuscleGroup(ExerciseMuscleGroupId exerciseMuscleGroupId, Exercise exercise, MuscleGroup muscleGroup, MuscleGroupType type)
    {
        Id = exerciseMuscleGroupId;
        ExerciseId = exercise.TableId ?? throw new ArgumentNullException(nameof(exercise));
        MuscleGroupId = muscleGroup.TableId ?? throw new ArgumentNullException(nameof(muscleGroup));
        MuscleGroupType = type;
    }
    
    public static ExerciseMuscleGroup Create(Exercise exercise, MuscleGroup muscleGroup, MuscleGroupType type)
    {
        return new ExerciseMuscleGroup(
            ExerciseMuscleGroupId.New(),
            exercise,
            muscleGroup,
            type);
    }
}
