using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;
using ThriveActiveWellness.Modules.Users.Tests.Integration.Constants;

namespace ThriveActiveWellness.Modules.Exercises.Tests.Integration.Abstractions;

public class TestAuthenticationHandler: AuthenticationHandler<AuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder)
        : base(options, logger, encoder)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        // Create a principal with test user claims
        Claim[] claims =
        [
            new(ClaimTypes.Name, "TestUser"),
            new(ClaimConstants.Oid, DefaultTestIds.UserId.ToString()),
        ];
        
        var identity = new ClaimsIdentity(claims, "Mock");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Mock");

        var result = AuthenticateResult.Success(ticket);
        return Task.FromResult(result);
    }
    
}
