using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;

namespace ThriveActiveWellness.Api.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();
        
        ApplyMigration<ExercisesDbContext>(scope);
        ApplyMigration<NotificationsDbContext>(scope);
        ApplyMigration<UsersDbContext>(scope);
    }
    
    private static void ApplyMigration<TDbContext>(IServiceScope scope)
        where TDbContext : DbContext
    {
        using TDbContext context = scope.ServiceProvider.GetRequiredService<TDbContext>();
    
        context.Database.Migrate();
    }
}
