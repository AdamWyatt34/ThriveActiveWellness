using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Common.Infrastructure.Inbox;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Clients;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Clients;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Equipment;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

public sealed class ExercisesDbContext(DbContextOptions<ExercisesDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<Client> Clients { get; set; }
    internal DbSet<Domain.Equipment.Equipment> Equipment { get; set; }
    internal DbSet<Exercise> Exercises { get; set; }
    internal DbSet<ExerciseMuscleGroup> ExerciseMuscleGroups { get; set; }
    internal DbSet<MuscleGroup> MuscleGroups { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Exercises);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new ClientConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentConfiguration());
        modelBuilder.ApplyConfiguration(new ExerciseMuscleGroupConfiguration());
        modelBuilder.ApplyConfiguration(new MuscleGroupConfiguration());
    }
}
