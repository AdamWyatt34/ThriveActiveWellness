using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.UpdateExercise;

public sealed record UpdateExerciseCommand(
    Guid ExerciseId,
    string Name,
    string Description,
    string Difficulty,
    Guid EquipmentId,
    List<MediaDto> Media,
    List<MuscleGroupDto> MuscleGroups) : ICommand; 
