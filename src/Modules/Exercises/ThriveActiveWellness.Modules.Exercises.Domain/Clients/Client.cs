using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Clients;

public sealed class Client : Entity
{

    private Client()
    {
    }

    public Guid Id { get; private set; }
    public string Email { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public int TableId { get; private set; }

    public static Client Create(
        Guid id,
        string firstName,
        string lastName,
        string emailAddress)
    {
        var user = new Client
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
