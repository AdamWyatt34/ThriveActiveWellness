using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.Users;

public sealed class UserProfileUpdatedDomainEvent(Guid userId, string firstName, string lastName, string emailAddress) : DomainEvent
{
    public Guid UserId { get; init; } = userId;

    public string FirstName { get; init; } = firstName;

    public string LastName { get; init; } = lastName;
    
    public string EmailAddress { get; init; } = emailAddress;
}
