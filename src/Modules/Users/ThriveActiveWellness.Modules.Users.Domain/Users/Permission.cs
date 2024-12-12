namespace ThriveActiveWellness.Modules.Users.Domain.Users;

public sealed class Permission(string code)
{
    public static readonly Permission GetUser = new("users:read");
    public static readonly Permission ModifyIfCurrentUser = new("users:modify:current");
    public static readonly Permission ModifyAllUsers = new("users:modify");
    public static readonly Permission GetEquipment = new("equipment:read");
    public static readonly Permission SearchEquipment = new("equipment:search");
    public static readonly Permission ModifyEquipment = new("equipment:update");
    public static readonly Permission CreateEquipment = new("equipment:create");
    public static readonly Permission DeleteEquipment = new("equipment:delete");
    public static readonly Permission CreateExercise = new("exercise:create");
    public static readonly Permission GetExercise = new("exercise:read");
    public static readonly Permission SearchExercise = new("exercise:search");
    public static readonly Permission ModifyExercise = new("exercise:update");

    public string Code { get; } = code;
}
