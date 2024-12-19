using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Exercises;

internal sealed class CreateExercise : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("exercises", async (Request request, ISender sender) =>
        {
            Result result = await sender.Send(new CreateExerciseCommand(
                request.ExerciseId,
                request.Name,
                request.Description,
                request.Difficulty,
                request.EquipmentId,
                request.Media,
                request.MuscleGroups));
           
            return result.Match(Results.Created, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateExercise)
        .WithTags(Tags.Exercises);
    }

    internal sealed class Request
    {
        public Guid ExerciseId { get; init; }
        public string Name { get; init; }
        public string Description { get; init; }
        public string Difficulty { get; init; }
        public Guid EquipmentId { get; init; }
        public List<MediaDto> Media { get; init; }
        public List<MuscleGroupDto> MuscleGroups { get; init; }
    }
} 
