using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Notifications.Domain.Notifications;
using ThriveActiveWellness.Modules.Notifications.Domain.Shared;

namespace ThriveActiveWellness.Modules.Notifications.Domain.Users;

public sealed class User : Entity
{

    private User()
    {
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int TableId { get; private set; }

    public static User Create(
        Guid id,
        string firstName,
        string lastName,
        string emailAddress)
    {
        var user = new User
        {
            Id = id,
            FirstName = firstName,
            LastName = lastName,
            Email = emailAddress,
        };
        
        return user;
    }
    
    public void Update(string firstName, string lastName, string emailAddress)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = emailAddress;
    }
}
