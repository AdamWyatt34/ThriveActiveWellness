using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Notifications;

internal sealed class NotificationConfiguration : IEntityTypeConfiguration<Notification>
{
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder.HasKey(n => n.Id);

        builder.Property(n => n.Type)
            .IsRequired();

        builder.Property(n => n.TemplateId)
            .HasMaxLength(200);
        
        builder.HasOne<User>()
            .WithMany()
            .HasPrincipalKey(u => u.Id)
            .HasForeignKey(n => n.UserId);    
    }
}
