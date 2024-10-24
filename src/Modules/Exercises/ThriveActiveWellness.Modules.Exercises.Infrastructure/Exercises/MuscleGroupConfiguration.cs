using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Exercises;

internal sealed class MuscleGroupConfiguration : IEntityTypeConfiguration<MuscleGroup>
{
    public void Configure(EntityTypeBuilder<MuscleGroup> builder)
    {
        builder.ToTable("muscle_groups");

        builder.HasKey(model => model.TableId);
        
        builder.Property(model => model.TableId)
            .HasConversion(model => model.Value,
                value => new MuscleGroupTableId(value));
        
        builder.Property(model => model.TableId)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(model => model.Id)
            .IsUnique();

        builder.Property(mg => mg.Id)
            .HasConversion(id => id.Value, value => new MuscleGroupId(value))
            .IsRequired();

        builder.Property(mg => mg.Name)
            .IsRequired()
            .HasMaxLength(255);
    }
}
