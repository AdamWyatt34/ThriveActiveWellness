using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.Users;

internal sealed class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("roles");

        builder.HasKey(r => r.Name);

        builder.Property(r => r.Name).HasMaxLength(50);

        builder
            .HasMany<User>()
            .WithMany(u => u.Roles)
            .UsingEntity(joinBuilder =>
            {
                joinBuilder.ToTable("user_roles");

                joinBuilder.Property("RolesName").HasColumnName("role_name");
            });

        builder.HasData(
            Role.Client,
            Role.Trainer,
            Role.MealService,
            Role.Administrator);
    }
}
