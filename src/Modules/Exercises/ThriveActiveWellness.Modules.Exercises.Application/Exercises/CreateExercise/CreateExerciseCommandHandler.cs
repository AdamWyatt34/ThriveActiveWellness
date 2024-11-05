using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;

internal sealed class CreateExerciseCommandHandler(
    IEquipmentRepository equipmentRepository,
    IMuscleGroupRepository muscleGroupRepository,
    IExerciseRepository exerciseRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<CreateExerciseCommand>
{
    public async Task<Result> Handle(CreateExerciseCommand command, CancellationToken cancellationToken)
    {
        // Get the equipment
        Domain.Equipment.Equipment? equipment = await equipmentRepository.GetByIdAsync(new EquipmentId(command.EquipmentId));
        
        if (equipment is null)
        {
            return Result.Failure(ExerciseErrors.EquipmentNotFound(command.EquipmentId));
        }

        // Create the exercise
        var exercise = Exercise.Create(
            new ExerciseId(command.ExerciseId),
            command.Name,
            command.Description,
            command.Difficulty,
            equipment);

        // Add media
        foreach (MediaDto mediaDto in command.Media)
        {
            exercise.AddMedia(mediaDto.Url, mediaDto.Description, mediaDto.Type);
        }

        var muscleGroupIds = command.MuscleGroups.Select(mg => new MuscleGroupId(mg.MuscleGroupId)).ToList();
        
        // Get muscle groups
        List<MuscleGroup> muscleGroups = await muscleGroupRepository.GetByIdsAsync(muscleGroupIds);
        
        // Add muscle groups
        foreach (MuscleGroupDto muscleGroupDto in command.MuscleGroups)
        {
            MuscleGroup muscleGroup = muscleGroups.Find(mg => mg.Id.Value == muscleGroupDto.MuscleGroupId);
            
            if (muscleGroup is null)
            {
                return Result.Failure(ExerciseErrors.MuscleGroupNotFound(muscleGroupDto.MuscleGroupId));
            }

            exercise.AddMuscleGroup(muscleGroup, muscleGroupDto.Type);
        }

        exerciseRepository.Add(exercise);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        
        return Result.Success();
    }
} 
