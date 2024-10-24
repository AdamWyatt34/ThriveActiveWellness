using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.UpdateEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Equipment;

internal sealed class UpdateEquipment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPut("equipment/{id}", async (Guid id, Request request, ISender sender) =>
        {
            Result result = await sender.Send(new UpdateEquipmentCommand(
                id,
                request.Name));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.ModifyEquipment)
        .WithTags(Tags.Equipment);
    }

    internal sealed class Request
    {
        public string Name { get; init; }
    }
}
