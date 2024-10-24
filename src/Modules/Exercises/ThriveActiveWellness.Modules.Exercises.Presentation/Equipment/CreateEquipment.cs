using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.CreateEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Equipment;

internal sealed class CreateEquipment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapPost("equipment", async (Request request, ISender sender) =>
        {
            Result<Guid> result = await sender.Send(new CreateEquipmentCommand(
                request.Name));
           
            return result.Match(Results.Created, ApiResults.Problem);
        })
        .RequireAuthorization(Permissions.CreateEquipment)
        .WithTags(Tags.Equipment);
    }

    internal sealed class Request
    {
        public string Name { get; init; }
    }
}
