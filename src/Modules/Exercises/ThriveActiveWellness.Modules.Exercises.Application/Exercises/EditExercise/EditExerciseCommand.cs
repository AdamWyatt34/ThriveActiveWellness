using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.EditExercise;

public sealed record EditExerciseCommand(
    Guid ExerciseId,
    string Name,
    string Description,
    string Difficulty,
    Guid EquipmentId,
    List<MediaDto> Media,
    List<MuscleGroupDto> MuscleGroups) : ICommand; 