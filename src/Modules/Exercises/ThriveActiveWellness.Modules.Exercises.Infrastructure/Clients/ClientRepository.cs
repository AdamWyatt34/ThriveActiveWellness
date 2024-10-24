using Microsoft.EntityFrameworkCore;
using ThriveActiveWellness.Modules.Exercises.Domain.Clients;
using ThriveActiveWellness.Modules.Exercises.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Exercises.Infrastructure.Clients;

internal sealed class ClientRepository(ExercisesDbContext context) : IClientRepository
{
    public async Task<Client?> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Clients.SingleOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public void Insert(Client attendee)
    {
        context.Clients.Add(attendee);
    }
}
