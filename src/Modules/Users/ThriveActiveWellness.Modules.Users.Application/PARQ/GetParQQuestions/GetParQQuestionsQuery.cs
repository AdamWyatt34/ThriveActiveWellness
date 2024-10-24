using ThriveActiveWellness.Common.Application.Messaging;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQQuestions;

public record GetParQQuestionsQuery(int? ParentQuestionId) : IQuery<IEnumerable<ParqQuestionResponse>>;
