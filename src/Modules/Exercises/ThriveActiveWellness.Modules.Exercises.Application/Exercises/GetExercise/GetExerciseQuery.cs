using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Exercises.Application.Exercises.GetExercise;

public record GetExerciseQuery(Guid Id) : IQuery<GetExerciseResponse>;
