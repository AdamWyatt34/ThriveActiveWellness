using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Infrastructure.Authentication;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Users.Application.Users.GetUser;

namespace ThriveActiveWellness.Modules.Users.Presentation.Users;

internal sealed class GetUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("users/profile", async (ClaimsPrincipal claims, ISender sender) =>
        {
            Result<UserResponse> result = await sender.Send(new GetUserQuery(claims.GetUserId()));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetUser)
        .WithTags(Tags.Users);
    }
}
