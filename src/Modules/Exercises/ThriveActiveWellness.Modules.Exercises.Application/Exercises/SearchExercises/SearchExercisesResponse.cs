using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.SearchExercises;

public sealed record SearchExercisesResponse(
    int Page,
    int PageSize,
    int TotalCount,
    IReadOnlyCollection<ExerciseResponse> Exercises
    );

public record ExerciseResponse(
    Guid ExerciseId,
    string Name,
    string Description,
    string Difficulty,
    EquipmentResponse Equipment,
    IReadOnlyCollection<string> Images,
    IReadOnlyCollection<MuscleGroupResponse> MuscleGroups
    );

public record MuscleGroupResponse(
    Guid MuscleGroupId,
    string Name
    );
    
