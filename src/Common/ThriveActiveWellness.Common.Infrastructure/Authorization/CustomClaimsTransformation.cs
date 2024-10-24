using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using ThriveActiveWellness.Common.Application.Authorization;
using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Infrastructure.Authentication;

namespace ThriveActiveWellness.Common.Infrastructure.Authorization;

internal sealed class CustomClaimsTransformation(IServiceScopeFactory serviceScopeFactory) : IClaimsTransformation
{
    public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        if (principal.HasClaim(c => c.Type == CustomClaims.TawUid))
        {
            return principal;
        }

        using IServiceScope scope = serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope.ServiceProvider.GetRequiredService<IPermissionService>();

        string identityId = principal.GetIdentityId();

        Result<PermissionsResponse> result = await permissionService.GetUserPermissionsAsync(identityId);

        if (result.IsFailure)
        {
            throw new ThriveActiveWellnessException(nameof(IPermissionService.GetUserPermissionsAsync), result.Error);
        }

        var claimsIdentity = new ClaimsIdentity();

        claimsIdentity.AddClaim(new Claim(CustomClaims.TawUid, result.Value.UserId.ToString()));

        foreach (string permission in result.Value.Permissions)
        {
            claimsIdentity.AddClaim(new Claim(CustomClaims.Permission, permission));
        }

        principal.AddIdentity(claimsIdentity);

        return principal;
    }
}
