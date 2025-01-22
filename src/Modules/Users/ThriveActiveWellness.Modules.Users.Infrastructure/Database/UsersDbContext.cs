using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Common.Infrastructure.Inbox;
using ThriveActiveWellness.Common.Infrastructure.Outbox;
using ThriveActiveWellness.Modules.Users.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Domain.Users;
using ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;
using ThriveActiveWellness.Modules.Users.Infrastructure.Users;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.Database;

public sealed class UsersDbContext(DbContextOptions<UsersDbContext> options) : DbContext(options), IUnitOfWork
{
    internal DbSet<ParQCompletion> ParqCompletions { get; set; }
    internal DbSet<ParqQuestion> ParqQuestions { get; set; }
    internal DbSet<ParqResponse> ParqResponses { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schemas.Users);

        modelBuilder.ApplyConfiguration(new OutboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new OutboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConfiguration());
        modelBuilder.ApplyConfiguration(new InboxMessageConsumerConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new RoleConfiguration());
        modelBuilder.ApplyConfiguration(new PermissionConfiguration());
        modelBuilder.ApplyConfiguration(new ParqQuestionConfiguration());
        modelBuilder.ApplyConfiguration(new ParqCompletionConfiguration());
        modelBuilder.ApplyConfiguration(new ParqResponseConfiguration());
    }
}
