using Refit;
using ThriveActiveWellness.UI.Models.ApiModels;

namespace ThriveActiveWellness.UI.Services.Clients;

/// <summary>
/// Generic interface for CRUD operations for an API
/// </summary>
/// <typeparam name="TGet">The type of the model returned by the GET operation</typeparam>
/// <typeparam name="TCreate">The type of the model used by the CREATE operation</typeparam>
/// <typeparam name="TCreateReturn">The type of the model returned by the CREATE operation</typeparam>
/// <typeparam name="TUpdate">The type of the model used by the UPDATE operation</typeparam>
[Headers("Authorization: Bearer")]
public interface ICrudApi<TGet, in TCreate, TCreateReturn, in TUpdate> 
    where TGet : class 
    where TCreate : class 
    where TCreateReturn : class 
    where TUpdate : class
{
    /// <summary>
    /// Get a single item by ID
    /// </summary>
    /// <param name="id">The ID of the item to get</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The TGet model</returns>
    [Get("/{id}")]
    Task<ResultModel<TGet>> GetAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Create a new item
    /// </summary>
    /// <param name="createModel">The model to create</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The TCreateReturn model</returns>
    [Post("")]
    Task<ResultModel<TCreateReturn>> CreateAsync([Body] TCreate createModel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing item
    /// </summary>
    /// <param name="id">The ID of the item to update</param>
    /// <param name="updateModel">The model to update</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task with ResultModel</returns>
    [Put("/{id}")]
    Task<ResultModel> UpdateAsync(Guid id, [Body] TUpdate updateModel, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an existing item
    /// </summary>
    /// <param name="id">The ID of the item to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    [Delete("/{id}")]
    Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
