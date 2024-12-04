using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Application.Exercises.CreateExercise;
using ThriveActiveWellness.Modules.Exercises.Domain.Equipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.UpdateExercise;

public sealed class UpdateExerciseCommandHandler(
    IExerciseRepository exerciseRepository,
    IEquipmentRepository equipmentRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateExerciseCommand>
{
    public async Task<Result> Handle(UpdateExerciseCommand request, CancellationToken cancellationToken)
    {
        Exercise? exercise = await exerciseRepository.GetByIdAsync(new ExerciseId(request.ExerciseId));
        
        if (exercise is null)
        {
            return Result.Failure(ExerciseErrors.ExerciseNotFound(request.ExerciseId));
        }
        
        Domain.Equipment.Equipment? equipment = await equipmentRepository.GetByIdAsync(new EquipmentId(request.EquipmentId));

        if (equipment is null)
        {
            return Result.Failure(ExerciseErrors.EquipmentNotFound(request.EquipmentId));
        }
        
        foreach (MediaDto mediaDto in request.Media)
        {
            exercise.AddMedia(
                mediaDto.Url,
                mediaDto.FileName,
                mediaDto.Description,
                mediaDto.Type);
        }
        
        exercise.Update(
            request.Name,
            request.Description,
            request.Difficulty,
            equipment.TableId,
            request.MuscleGroups.Select(mg => MuscleGroup.Create(new MuscleGroupId(mg.MuscleGroupId), mg.Name)).ToList());

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
} 
