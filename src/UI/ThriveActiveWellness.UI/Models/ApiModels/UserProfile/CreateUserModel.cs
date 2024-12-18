namespace ThriveActiveWellness.UI.Models.ApiModels.UserProfile;

public sealed record CreateUserModel(
    string? Email,
    string? FirstName,
    string? LastName,
    string? IdentityId);
