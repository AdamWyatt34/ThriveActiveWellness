using ThriveActiveWellness.Common.Application.Enums;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Modules.Exercises.Domain.Exercises;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.SearchExercises;

public record SearchExercisesQuery(
    string? Search,
    int Page,
    int PageSize,
    ListExercisesSortOptions SortOption,
    SortDirection SortDirection
    ) : IQuery<SearchExercisesResponse>;
