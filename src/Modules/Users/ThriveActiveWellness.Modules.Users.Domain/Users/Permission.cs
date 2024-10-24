namespace ThriveActiveWellness.Modules.Users.Domain.Users;

public sealed class Permission
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyIfCurrentUser = new("users:modify:current");
    public static readonly Permission ModifyAllUsers = new("users:modify");
    public static readonly Permission GetEquipment = new("equipment:read");
    public static readonly Permission SearchEquipment = new("equipment:search");
    public static readonly Permission ModifyEquipment = new("equipment:update");
    public static readonly Permission CreateEquipment = new("equipment:create");
    public static readonly Permission DeleteEquipment = new("equipment:delete");

    public Permission(string code)
    {
        Code = code;
    }

    public string Code { get; }
}
