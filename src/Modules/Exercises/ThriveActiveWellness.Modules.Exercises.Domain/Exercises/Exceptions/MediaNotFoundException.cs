namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises.Exceptions;

public class MediaNotFoundException : Exception
{
    public MediaNotFoundException(Guid exerciseId)
        : base($"Media for exercise with id {exerciseId} was not found.")
    {
    }
}
