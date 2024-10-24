using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Infrastructure.Authentication;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Users.Application.Users.UpdateUser;

namespace ThriveActiveWellness.Modules.Users.Presentation.Users;

internal sealed class UpdateUserProfile : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("users/profile", async (Request request,  ClaimsPrincipal claims, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateUserCommand(
                claims.GetUserId(),
                request.FirstName,
                request.LastName,
                request.Email));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyUser)
        .WithTags(Tags.Users);
    }

    internal sealed class Request
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }
        
        public string Email { get; init; }
    }
}
