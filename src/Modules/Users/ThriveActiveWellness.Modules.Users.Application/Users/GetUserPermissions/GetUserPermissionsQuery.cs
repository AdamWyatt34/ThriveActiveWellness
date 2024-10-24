using ThriveActiveWellness.Common.Application.Authorization;
using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.Users.GetUserPermissions;

public sealed record GetUserPermissionsQuery(string IdentityId) : IQuery<PermissionsResponse>;
