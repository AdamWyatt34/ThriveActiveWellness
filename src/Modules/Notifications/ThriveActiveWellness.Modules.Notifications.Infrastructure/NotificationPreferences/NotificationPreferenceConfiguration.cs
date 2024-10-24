using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Notifications.Domain.NotificationPreference;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.NotificationPreferences;

public class NotificationPreferenceConfiguration : IEntityTypeConfiguration<NotificationPreference>
{
    public void Configure(EntityTypeBuilder<NotificationPreference> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Type)
            .IsRequired();
        
        builder.HasIndex(n => new { n.UserTableId, n.Type })
            .IsUnique();
        
        builder.HasOne<User>().WithMany().HasPrincipalKey(u => u.TableId).HasForeignKey(n => n.UserTableId);
        
    }
}
