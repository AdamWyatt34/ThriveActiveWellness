using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Common.Infrastructure.Inbox;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Notifications.Application.Abstractions;
using ThriveActiveWellness.Modules.Notifications.Domain.NotificationPreference;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.NotificationPreferences;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Notifications;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Users;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;

public sealed class NotificationsDbContext(DbContextOptions<NotificationsDbContext> options) 
    : DbContext(options), IUnitOfWork
{
    internal DbSet<Notification> Notifications { get; set; }
    internal DbSet<NotificationPreference> NotificationPreferences { get; set; }
    internal DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Notifications);
        
        modelBuilder.ApplyConfiguration(new NotificationConfiguration());
        modelBuilder.ApplyConfiguration(new NotificationPreferenceConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
    }
}
