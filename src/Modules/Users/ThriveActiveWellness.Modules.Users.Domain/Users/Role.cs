namespace ThriveActiveWellness.Modules.Users.Domain.Users;

public sealed class Role
{
    public static readonly Role Administrator = new("Administrator");
    public static readonly Role Trainer = new("Trainer");
    public static readonly Role Client = new("Client");
    public static readonly Role MealService = new("MealService");

    private Role(string name)
    {
        Name = name;
    }

    private Role()
    {
    }

    public string Name { get; private set; }
}
