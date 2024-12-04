using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.GetExercise;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Exercises;

internal sealed class GetExercise : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("exercises/{id}", async (Guid id, ISender sender) =>
        {
            Result<GetExerciseResponse> result = await sender.Send(new GetExerciseQuery(id));
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetExercise)
        .WithTags(Tags.Exercises);
    }
} 