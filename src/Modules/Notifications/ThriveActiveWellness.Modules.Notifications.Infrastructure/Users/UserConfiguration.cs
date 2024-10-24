using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Users;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(user => user.TableId);

        builder.Property(user => user.TableId).UseIdentityColumn();

        builder.Property(user => user.Id).IsRequired();

        builder.HasIndex(model => model.Id)
            .IsUnique();

        builder.Property(c => c.FirstName).HasMaxLength(200);

        builder.Property(c => c.LastName).HasMaxLength(200);

        builder.Property(c => c.Email).HasMaxLength(300);
    }
}
