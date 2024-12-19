using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;
using ThriveActiveWellnessAPI.Domain.Shared;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;

public sealed record MediaDto(Uri Url, string FileName, string Description, MediaType Type);

public sealed record MuscleGroupDto(Guid MuscleGroupId, string Name, MuscleGroupType Type);

public sealed record CreateExerciseCommand(
    Guid ExerciseId,
    string Name,
    string Description,
    string Difficulty,
    Guid EquipmentId,
    List<MediaDto> Media,
    List<MuscleGroupDto> MuscleGroups) : ICommand; 
