using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

internal sealed class ExerciseRepository(ExercisesDbContext dbContext) : IExerciseRepository
{
    public async Task<Exercise> GetByIdAsync(ExerciseId id)
    {
        return await dbContext.Exercises.SingleOrDefaultAsync(e => e.Id == id);
    }

    public void Add(Exercise exercise)
    {
        dbContext.Exercises.Add(exercise);
    }

    public void Update(Exercise exercise)
    {
        dbContext.Exercises.Update(exercise);
    }

    public void Delete(Exercise exercise)
    {
        dbContext.Exercises.Remove(exercise);
    }
}
