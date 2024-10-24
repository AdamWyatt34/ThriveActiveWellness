using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

internal sealed class ExerciseConfiguration : IEntityTypeConfiguration<Exercise>
{

    public void Configure(EntityTypeBuilder<Exercise> builder)
    {
        builder.ToTable("exercises");
        
        builder.HasKey(exercise => exercise.TableId);

        builder.Property(model => model.TableId)
            .HasConversion(model => model.Value,
                value => new ExerciseTableId(value));
        
        builder.Property(exercise => exercise.TableId)
            .ValueGeneratedOnAdd();
        
        builder.Property(exercise => exercise.Id)
            .HasConversion(exerciseId => exerciseId.Value, value => new ExerciseId(value))
            .IsRequired();
        
        builder.HasIndex(model => model.Id)
            .IsUnique();
        
        builder.Property(exercise => exercise.Name)
            .IsRequired()
            .HasMaxLength(255);
        
        builder.Property(exercise => exercise.Description);
        
        builder.Property(exercise => exercise.Difficulty)
            .HasMaxLength(20);
        
        builder.Property(exercise => exercise.EquipmentTableId)
            .HasConversion(id => id.Value, value => new EquipmentTableId(value));
        
        builder.HasOne<Domain.Equipment.Equipment>()
            .WithMany()
            .HasForeignKey(exercise => exercise.EquipmentTableId);

        builder.OwnsMany(exercise => exercise.Media)
            .ToTable("exercise_media")
            .Property(media => media.Url)
            .HasMaxLength(1000);
        
        builder.OwnsMany(exercise => exercise.Media)
            .ToTable("exercise_media")
            .Property(media => media.Description)
            .HasMaxLength(500);

        // Configuration for Exercise to ExerciseMuscleGroup relationship
        builder.HasMany(e => e.ExerciseMuscleGroups)
            .WithOne()
            .HasForeignKey(emg => emg.ExerciseId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
