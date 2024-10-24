using System.Security.Claims;
using Microsoft.Identity.Web;
using ThriveActiveWellness.Common.Application.Exceptions;

namespace ThriveActiveWellness.Common.Infrastructure.Authentication;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal? principal)
    {
        string? userId = principal?.FindFirst(CustomClaims.TawUid)?.Value;

        return Guid.TryParse(userId, out Guid parsedUserId) ?
            parsedUserId :
            throw new ThriveActiveWellnessException("User identifier is unavailable");
    }

    public static string GetIdentityId(this ClaimsPrincipal? principal)
    {
        // Dev note: Verify this is correct, was NameIdentifier in the original code
        return principal?.FindFirst(ClaimConstants.Oid)?.Value ??
               throw new ThriveActiveWellnessException("User identity is unavailable");
    }

    public static HashSet<string> GetPermissions(this ClaimsPrincipal? principal)
    {
        IEnumerable<Claim> permissionClaims = principal?.FindAll(CustomClaims.Permission) ??
                                              throw new ThriveActiveWellnessException("Permissions are unavailable");

        return permissionClaims.Select(c => c.Value).ToHashSet();
    }
}
