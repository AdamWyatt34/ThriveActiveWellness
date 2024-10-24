using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Domain.Users;

public sealed class User : Entity
{
    private readonly List<Role> _roles = [];

    private User()
    {
    }

    public Guid Id { get; private set; }

    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string IdentityId { get; private set; }
    public UserFitnessProfile FitnessProfile { get; private set; }
    public UserParQ ParQ { get; private set; }
    public int TableId { get; private set; }

    public IReadOnlyCollection<Role> Roles => _roles.ToList();

    public static User Create(
        string firstName,
        string lastName,
        string emailAddress,
        string identityId,
        Role role)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = firstName,
            LastName = lastName,
            Email = emailAddress,
            IdentityId = identityId
        };

        user._roles.Add(role);

        user.Raise(new UserRegisteredDomainEvent(user.Id));

        return user;
    }

    public void Update(string firstName, string lastName, string emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = emailAddress;

        Raise(new UserProfileUpdatedDomainEvent(Id, FirstName, LastName, Email));
    }
}
