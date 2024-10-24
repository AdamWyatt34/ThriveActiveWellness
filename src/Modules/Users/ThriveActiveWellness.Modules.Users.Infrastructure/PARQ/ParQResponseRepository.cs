using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;

public class ParQResponseRepository(UsersDbContext dbContext) : IParQResponseRepository
{
    public void Insert(ParqResponse response)
    {
        dbContext.ParqResponses.Add(response);
    }
}
