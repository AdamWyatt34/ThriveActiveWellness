using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

internal sealed class MuscleGroupRepository(ExercisesDbContext dbContext) : IMuscleGroupRepository
{
    public async Task<MuscleGroup> GetByIdAsync(MuscleGroupId id)
    {
        return await dbContext.MuscleGroups.SingleOrDefaultAsync(mg => mg.Id == id);
    }

    public async Task<List<MuscleGroup>> GetByIdsAsync(IEnumerable<MuscleGroupId> ids)
    {
        return await dbContext.MuscleGroups
            .Where(mg => ids.Contains(mg.Id))
            .ToListAsync();
    }

    public void Add(MuscleGroup muscleGroup)
    {
        dbContext.MuscleGroups.Add(muscleGroup);
    }

    public void Update(MuscleGroup muscleGroup)
    {
        dbContext.MuscleGroups.Update(muscleGroup);
    }

    public void Delete(MuscleGroup muscleGroup)
    {
        dbContext.MuscleGroups.Remove(muscleGroup);
    }
}
