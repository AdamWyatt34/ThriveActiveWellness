using Refit;
using ThriveActiveWellness.UI.Enums;
using ThriveActiveWellness.UI.Models.ApiModels;
using ThriveActiveWellness.UI.Models.ApiModels.Equipment;

namespace ThriveActiveWellness.UI.Services.Clients;

/// <summary>
/// Equipment API client
/// </summary>
public interface IEquipmentApi : ICrudApi<EquipmentModel, CreateEquipmentRequest, EquipmentModel, UpdateEquipmentRequest>
{
    /// <summary>
    /// List equipment with pagination and sorting
    /// </summary>
    [Get("")]
    Task<ResultModel<PagedResultModel<EquipmentModel>>> ListEquipmentAsync(
        [Query] string search,
        [Query] ListEquipmentSortOptions sortOptions,
        [Query] SortDirection sortDirection,
        [Query] int page,
        [Query] int pageSize,
        CancellationToken cancellationToken = default
    );
}
