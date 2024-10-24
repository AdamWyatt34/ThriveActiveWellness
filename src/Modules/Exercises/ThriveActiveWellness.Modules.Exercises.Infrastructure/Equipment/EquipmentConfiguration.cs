using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Equipment;

internal sealed class EquipmentConfiguration : IEntityTypeConfiguration<Domain.Equipment.Equipment>
{

    public void Configure(EntityTypeBuilder<Domain.Equipment.Equipment> builder)
    {
        builder.ToTable("equipment");
        
        builder.HasKey(model => model.TableId);
        
        builder.Property(model => model.TableId)
            .HasConversion(model => model.Value,
                value => new EquipmentTableId(value));
        
        builder.Property(model => model.TableId)
            .ValueGeneratedOnAdd();
        
        builder.HasIndex(model => model.Id)
            .IsUnique();
        
        builder.Property(model => model.Id)
            .HasConversion(model => model.Value, value => new EquipmentId(value))
            .IsRequired();

        builder.Property(equipment => equipment.Name)
            .HasMaxLength(255);
        
        builder.Property(equipment => equipment.IsActive)
            .HasDefaultValue(true)
            .IsRequired();
        
        builder.HasQueryFilter(equipment => equipment.IsActive);
    }
}
