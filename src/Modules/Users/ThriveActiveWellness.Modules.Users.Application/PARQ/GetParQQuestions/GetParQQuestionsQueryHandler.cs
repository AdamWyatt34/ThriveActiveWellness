using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQQuestions;

internal sealed class GetParQQuestionsQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetParQQuestionsQuery, IEnumerable<ParqQuestionResponse>>
{
    public async Task<Result<IEnumerable<ParqQuestionResponse>>> Handle(GetParQQuestionsQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql = $"""
                           SELECT
                               id AS {nameof(ParqQuestionResponse.Id)},
                               question AS {nameof(ParqQuestionResponse.Question)}
                           FROM users.parq_questions
                           WHERE parent_question_id = @ParentQuestionId
                           """;

        var parameters = new { request.ParentQuestionId };

        IEnumerable<ParqQuestionResponse> questions = await connection.QueryAsync<ParqQuestionResponse>(sql, parameters);

        return Result.Success(questions);
    }
}
