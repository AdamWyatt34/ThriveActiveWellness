using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Common.Application.Authorization;

public interface IPermissionService
{
    Task<Result<PermissionsResponse>> GetUserPermissionsAsync(string identityId);
}
