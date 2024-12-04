using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.GetExercise;

internal sealed class GetExerciseQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<GetExerciseQuery, GetExerciseResponse>
{
    public async Task<Result<GetExerciseResponse>> Handle(GetExerciseQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();
        
        const string sql = $"""
            SELECT 
                e.id AS {nameof(GetExerciseResponse.Id)},
                e.name AS {nameof(GetExerciseResponse.Name)},
                e.description AS {nameof(GetExerciseResponse.Description)},
                e.instructions AS {nameof(GetExerciseResponse.Instructions)},
                e.notes AS {nameof(GetExerciseResponse.Notes)},
                e.difficulty AS {nameof(GetExerciseResponse.Difficulty)},
                e.category AS {nameof(GetExerciseResponse.Category)},
                e.equipment AS {nameof(GetExerciseResponse.Equipment)},
                e.muscle_group AS {nameof(GetExerciseResponse.MuscleGroup)},
                m.id AS {nameof(GetExerciseResponse.MediaDto.Id)},
                m.url AS {nameof(GetExerciseResponse.MediaDto.Url)},
                m.type AS {nameof(GetExerciseResponse.MediaDto.Type)},
                m.thumbnail_url AS {nameof(GetExerciseResponse.MediaDto.ThumbnailUrl)}
            FROM exercises.exercises e
            LEFT JOIN exercises.media m ON m.exercise_id = e.id
            WHERE e.id = @Id
            """;

        var exerciseDict = new Dictionary<Guid, GetExerciseResponse>();
        
        await connection.QueryAsync<GetExerciseResponse, GetExerciseResponse.MediaDto, GetExerciseResponse>(
            sql,
            (exercise, media) =>
            {
                if (!exerciseDict.TryGetValue(exercise.Id, out GetExerciseResponse? exerciseEntry))
                {
                    exerciseEntry = exercise;
                    exerciseEntry.Media = new List<GetExerciseResponse.MediaDto>();
                    exerciseDict.Add(exerciseEntry.Id, exerciseEntry);
                }

                if (media is not null)
                {
                    exerciseEntry.Media.Add(media);
                }

                return exerciseEntry;
            },
            new { request.Id },
            splitOn: $"{nameof(GetExerciseResponse.MediaDto.Id)}");
        
        GetExerciseResponse? result = exerciseDict.Values.FirstOrDefault();
        
        if (result is null)
        {
            return Result.Failure<GetExerciseResponse>(ExerciseErrors.ExerciseNotFound(request.Id));
        }
        
        return result;
    }
}
