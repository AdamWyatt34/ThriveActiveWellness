using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.UpdateExercise;
using ThriveActiveWellnessAPI.Domain.Shared;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Exercises;

internal sealed class UpdateExercise : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("exercises/{id}", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateExerciseCommand(
                id,
                request.Name,
                request.Description,
                request.Difficulty,
                request.EquipmentId,
                request.Media,
                request.MuscleGroups));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyExercise)
        .WithTags(Tags.Exercises);
    }

    internal sealed class Request
    {
        public string Name { get; init; }
        public string Description { get; init; }
        public string Difficulty { get; init; }
        public Guid EquipmentId { get; init; }
        public List<MediaDto> Media { get; init; }
        public List<MuscleGroupDto> MuscleGroups { get; init; }
    }
} 
