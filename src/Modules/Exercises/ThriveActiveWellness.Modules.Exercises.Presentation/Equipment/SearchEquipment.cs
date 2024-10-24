using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Presentation.Endpoints;
using ThriveActiveWellness.Common.Presentation.Results;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.SearchEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Presentation.Equipment;

internal sealed class SearchEquipment : IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("equipment", async (ISender sender, string? search, int page = 0, int pageSize = 10) =>
            {
                Result<SearchEquipmentResponse> result = await sender.Send(new SearchEquipmentQuery(
                    search,
                    page,
                    pageSize));

                return result.Match(Results.Ok, ApiResults.Problem);
            })
            .RequireAuthorization(Permissions.GetEquipment)
            .WithTags(Tags.Equipment);
    }
}
