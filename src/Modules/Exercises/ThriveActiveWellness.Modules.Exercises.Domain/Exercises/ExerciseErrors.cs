using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

public static class ExerciseErrors
{
    public static Error ExerciseNotFound(Guid exerciseId) => new(
        "Exercise.ExerciseNotFound",
        $"Exercise with ID {exerciseId} was not found.",
        ErrorType.Validation);
    
    public static Error EquipmentNotFound(Guid equipmentId) => new(
        "Exercise.EquipmentNotFound",
        $"Equipment with ID {equipmentId} was not found.",
        ErrorType.Validation);
        
    public static Error MuscleGroupNotFound(Guid muscleGroupId) => new(
        "Exercise.MuscleGroupNotFound",
        $"Muscle group with ID {muscleGroupId} was not found.",
        ErrorType.Validation);
} 
