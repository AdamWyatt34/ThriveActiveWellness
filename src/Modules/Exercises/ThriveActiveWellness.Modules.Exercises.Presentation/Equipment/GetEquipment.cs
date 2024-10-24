using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Equipment;

internal sealed class GetEquipment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("equipment/{id}", async (Guid id, ISender sender) =>
        {
            Result<EquipmentResponse> result = await sender.Send(new GetEquipmentQuery(id));
            
            return result.Match(Results.Ok, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.GetEquipment)
        .WithTags(Tags.Equipment);
    }
}
