using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellnessAPI.Domain.Shared;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public sealed class MediaUploadedDomainEvent : DomainEvent
{
    public Guid ExerciseId { get; }
    public Uri TemporaryUrl { get; }
    public string FileName { get; }
    public string Description { get; }
    public MediaType Type { get; }

    public MediaUploadedDomainEvent(
        Guid exerciseId,
        Uri temporaryUrl,
        string fileName,
        string description,
        MediaType type)
    {
        ExerciseId = exerciseId;
        TemporaryUrl = temporaryUrl;
        FileName = fileName;
        Description = description;
        Type = type;
    }
} 
