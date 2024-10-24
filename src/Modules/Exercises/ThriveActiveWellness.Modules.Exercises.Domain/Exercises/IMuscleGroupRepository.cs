namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public interface IMuscleGroupRepository
{
    Task<MuscleGroup> GetByIdAsync(MuscleGroupId id);
    void Add(MuscleGroup muscleGroup);
    void Update(MuscleGroup muscleGroup);
    void Delete(MuscleGroup muscleGroup);
}
