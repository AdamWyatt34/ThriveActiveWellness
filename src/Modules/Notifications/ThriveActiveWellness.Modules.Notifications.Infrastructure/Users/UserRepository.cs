using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Notifications.Domain.Users;
using ThriveActiveWellness.Modules.Notifications.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Notifications.Infrastructure.Users;

public class UserRepository(NotificationsDbContext context) : IUserRepository
{
    public async Task<User?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Users.SingleOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public void Insert(User user)
    {
        context.Users.Add(user);
    }
}
