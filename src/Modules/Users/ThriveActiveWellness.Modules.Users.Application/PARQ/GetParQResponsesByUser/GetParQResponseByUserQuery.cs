using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQResponsesByUser;

public record GetParQResponseByUserQuery(Guid UserId) : IQuery<IEnumerable<ParqResponseRecord>>;
