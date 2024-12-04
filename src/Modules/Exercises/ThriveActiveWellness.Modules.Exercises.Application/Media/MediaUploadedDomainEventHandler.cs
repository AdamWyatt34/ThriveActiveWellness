using ThriveActiveWellness.Common.Application.Exceptions;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.MediaStorage;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Media;

public class MediaUploadedDomainEventHandler(
    IStorageService storageService,
    IExerciseRepository exerciseRepository,
    IUnitOfWork unitOfWork)
    : DomainEventHandler<MediaUploadedDomainEvent>
{
    public override async Task Handle(MediaUploadedDomainEvent domainEvent, CancellationToken cancellationToken = default)
    {
        Exercise? exercise = await exerciseRepository.GetByIdAsync(new ExerciseId(domainEvent.ExerciseId));
        if (exercise is null)
        {
            throw new ThriveActiveWellnessException(
                nameof(MediaUploadedDomainEventHandler),
                new Error("Exercise.NotFound",$"Exercise with ID {domainEvent.ExerciseId} not found", ErrorType.NotFound));
        }

        // Update to move the file name based on the exercise ID
        Uri permanentUrl = storageService.MoveAsync(domainEvent.FileName, domainEvent.FileName);

        // Update the media URL in the exercise
        exercise.UpdateMediaUrl(domainEvent.TemporaryUrl, permanentUrl);

        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
