using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.DeleteEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Equipment;

internal sealed class DeleteEquipment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapDelete("equipment/{id}", async (Guid id, ISender sender) =>
        {
            Result result = await sender.Send(new DeleteEquipmentCommand(id));

            return result.Match(Results.NoContent, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.DeleteEquipment)
        .WithTags(Tags.Equipment);
    }
}
