using Refit;
using ThriveActiveWellness.UI.Models.ApiModels;
using ThriveActiveWellness.UI.Models.ApiModels.UserProfile;

namespace ThriveActiveWellness.UI.Services.Clients;

/// <summary>
/// User profile API client
/// </summary>
public interface IUserProfileApi
{
    /// <summary>
    /// Create a new user profile
    /// Sends the Bearer token in the header for authorization
    /// </summary>
    /// <param name="createModel">The model to create the user profile with</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task</returns>
    [Post("/register")]
    Task CreateAsync([Body] CreateUserModel createModel, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get a single user profile by ID
    /// </summary>
    /// <param name="id">The ID of the user profile to get</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns><see cref="UserProfileDetailModel"/></returns>
    [Headers("Authorization: Bearer")]
    [Get("/{id}")]
    Task<UserProfileDetailModel> GetAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update an existing user profile
    /// </summary>
    /// <param name="id">The ID of the user profile to update</param>
    /// <param name="updateModel">The model to update the user profile with</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Task with <see cref="ResultModel"/></returns>
    [Headers("Authorization: Bearer")]
    [Put("/{id}")]
    Task<ResultModel> UpdateAsync(Guid id, [Body] UpdateUserProfileModel updateModel, CancellationToken cancellationToken = default);
}
