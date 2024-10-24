using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public interface IExerciseRepository
{
    Task<Exercise> GetByIdAsync(ExerciseId id);
    void Add(Exercise exercise);
    void Update(Exercise exercise);
    void Delete(Exercise exercise);
}
