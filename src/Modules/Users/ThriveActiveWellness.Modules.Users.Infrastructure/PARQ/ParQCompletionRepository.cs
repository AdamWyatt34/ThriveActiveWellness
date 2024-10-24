using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Infrastructure.Database;

namespace ThriveActiveWellness.Modules.Users.Infrastructure.PARQ;

public class ParQCompletionRepository(UsersDbContext dbContext) : IParQCompletionRepository
{
    public void Insert(ParQCompletion completion)
    {
        dbContext.ParqCompletions.Add(completion);
    }
}
