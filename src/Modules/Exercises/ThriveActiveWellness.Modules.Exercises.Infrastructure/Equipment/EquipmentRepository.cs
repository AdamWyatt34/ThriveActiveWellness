using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Equipment;

internal sealed class EquipmentRepository(ExercisesDbContext dbContext) : IEquipmentRepository
{
    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        return await dbContext.Equipment.AnyAsync(e => e.Name == name, cancellationToken);
    }
    public async Task<bool> IsNameUniqueAsync(EquipmentId equipmentId, string name, CancellationToken cancellationToken)
    {
        return await dbContext.Equipment.AnyAsync(e => e.Name == name && e.Id != equipmentId, cancellationToken);
    }

    public async Task<Domain.Equipment.Equipment> GetByIdAsync(EquipmentId id)
    {
        return await dbContext.Equipment.SingleOrDefaultAsync(e => e.Id == id);
    }

    public void Add(Domain.Equipment.Equipment equipment)
    {
        dbContext.Equipment.Add(equipment);
    }

    public void Update(Domain.Equipment.Equipment equipment)
    {
        dbContext.Equipment.Update(equipment);
    }

    public void Delete(Domain.Equipment.Equipment equipment)
    {
        dbContext.Equipment.Remove(equipment);
    }
}
