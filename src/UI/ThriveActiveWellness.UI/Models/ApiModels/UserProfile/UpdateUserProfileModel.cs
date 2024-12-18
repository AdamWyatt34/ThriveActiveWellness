using ThriveActiveWellness.UI.Enums;

namespace ThriveActiveWellness.UI.Models.ApiModels.UserProfile;

public sealed record UpdateUserProfileModel(
    string FirstName,
    string LastName,
    string EmailAddress,
    string PhoneNumber,
    string StreetAddress,
    string City,
    string State,
    string ZipCode,
    DateTime DateOfBirth,
    string Gender,
    string FitnessGoals,
    FitnessLevel FitnessLevel,
    string HealthInformation,
    string DietaryPreferences,
    string ParQResponses
);
