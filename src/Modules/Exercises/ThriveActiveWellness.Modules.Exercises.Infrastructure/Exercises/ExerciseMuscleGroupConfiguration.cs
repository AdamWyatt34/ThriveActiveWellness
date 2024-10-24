using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

internal sealed class ExerciseMuscleGroupConfiguration : IEntityTypeConfiguration<ExerciseMuscleGroup>
{
    public void Configure(EntityTypeBuilder<ExerciseMuscleGroup> builder)
    {
        builder.ToTable("exercise_muscle_groups");

        builder.HasKey(model => model.TableId);
        
        builder.Property(model => model.TableId)
            .HasConversion(model => model.Value,
                value => new ExerciseMuscleGroupTableId(value));
        
        builder.Property(model => model.TableId)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(model => model.Id)
            .IsUnique();

        builder.Property(emg => emg.Id)
            .HasConversion(id => id.Value, value => new ExerciseMuscleGroupId(value))
            .IsRequired();

        builder.Property(emg => emg.ExerciseId)
            .HasConversion(id => id.Value, value => new ExerciseTableId(value));

        builder.Property(emg => emg.MuscleGroupId)
            .HasConversion(id => id.Value, value => new MuscleGroupTableId(value));

        builder.HasOne<Exercise>()
            .WithMany(e => e.ExerciseMuscleGroups)
            .HasForeignKey(emg => emg.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<MuscleGroup>()
            .WithMany()
            .HasForeignKey(emg => emg.MuscleGroupId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property(emg => emg.MuscleGroupType).IsRequired();
    }

}
