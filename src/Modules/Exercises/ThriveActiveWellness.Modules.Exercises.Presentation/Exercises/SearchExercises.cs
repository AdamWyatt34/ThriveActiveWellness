using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Application.Enums;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.SearchExercises;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Exercises;

internal sealed class SearchExercises : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("exercises", async (
            ISender sender,
            string? search,
            int page = 0,
            int pageSize = 10,
            ListExercisesSortOptions sortOption = ListExercisesSortOptions.Name,
            SortDirection sortDirection = SortDirection.Asc) =>
        {
            Result<SearchExercisesResponse> result = await sender.Send(new SearchExercisesQuery(
                search,
                page,
                pageSize,
                sortOption,
                sortDirection));

            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.SearchExercise)
        .WithTags(Tags.Exercises);
    }
} 
