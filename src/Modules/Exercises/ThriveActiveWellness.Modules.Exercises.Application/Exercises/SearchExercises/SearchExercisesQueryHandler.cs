using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Common.Application.Enums;
using ThriveActiveWellness.Modules.Exercises.Application.Equipment.GetEquipment;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.SearchExercises;

public class SearchExercisesQueryHandler(IDbConnectionFactory dbConnectionFactory) 
    : IQueryHandler<SearchExercisesQuery, SearchExercisesResponse>
{
    public async Task<Result<SearchExercisesResponse>> Handle(SearchExercisesQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        string orderByClause = GetOrderByClause(request.SortOption, request.SortDirection);
        
        string sql = $"""
            WITH exercise_data AS (
                SELECT 
                    e.id AS {nameof(ExerciseResponse.ExerciseId)},
                    e.name AS {nameof(ExerciseResponse.Name)},
                    e.description AS {nameof(ExerciseResponse.Description)},
                    e.difficulty AS {nameof(ExerciseResponse.Difficulty)},
                    eq.id AS "{nameof(ExerciseResponse.Equipment)}.{nameof(EquipmentResponse.EquipmentId)}",
                    eq.name AS "{nameof(ExerciseResponse.Equipment)}.{nameof(EquipmentResponse.Name)}",
                    array_agg(DISTINCT em.url) AS {nameof(ExerciseResponse.Images)},
                    array_agg(DISTINCT jsonb_build_object(
                        'muscleGroupId', mg.id,
                        'name', mg.name
                    )) AS muscle_groups
                FROM exercises.exercises e
                LEFT JOIN exercises.equipment eq ON e.equipment_id = eq.id
                LEFT JOIN exercises.exercise_media em ON e.id = em.exercise_id
                LEFT JOIN exercises.exercise_muscle_groups emg ON e.id = emg.exercise_id
                LEFT JOIN exercises.muscle_groups mg ON emg.muscle_group_id = mg.id
                WHERE (@Search IS NULL OR e.name ILIKE '%' || @Search || '%')
                GROUP BY e.id, e.name, e.description, e.difficulty, eq.id, eq.name
                {orderByClause}
                OFFSET @Offset
                LIMIT @PageSize
            )
            SELECT 
                ExerciseId,
                Name,
                Description,
                Difficulty,
                "{nameof(ExerciseResponse.Equipment)}.{nameof(EquipmentResponse.EquipmentId)}" AS "Equipment.EquipmentId",
                "{nameof(ExerciseResponse.Equipment)}.{nameof(EquipmentResponse.Name)}" AS "Equipment.Name",
                Images,
                muscle_groups AS {nameof(ExerciseResponse.MuscleGroups)}
            FROM exercise_data
            """;

        (string? Search, int Offset, int PageSize) parameters = (request.Search, Offset: (request.Page - 1) * request.PageSize, request.PageSize);

        IEnumerable<ExerciseResponse> exercises = await connection.QueryAsync<ExerciseResponse>(sql, parameters);

        const string countSql = """
            SELECT COUNT(DISTINCT e.id)
            FROM exercises.exercises e
            WHERE (@Search IS NULL OR e.name ILIKE '%' || @Search || '%')
            """;

        int totalCount = await connection.ExecuteScalarAsync<int>(countSql, new { request.Search });

        return new SearchExercisesResponse(
            request.Page,
            request.PageSize,
            totalCount,
            exercises.ToList());
    }

    private static string GetOrderByClause(ListExercisesSortOptions sortOption, SortDirection sortDirection)
    {
        string direction = sortDirection == SortDirection.Asc ? "ASC" : "DESC";
        
        return sortOption switch
        {
            ListExercisesSortOptions.Name => $"ORDER BY e.name {direction}",
            ListExercisesSortOptions.Difficulty => $"ORDER BY e.difficulty {direction}",
            _ => $"ORDER BY e.name {direction}"
        };
    }
}
