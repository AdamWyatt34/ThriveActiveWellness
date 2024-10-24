using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.GetParQResponsesByUser;

internal sealed class GetParQResponseByUserQueryHandler(IDbConnectionFactory dbConnectionFactory) : IQueryHandler<GetParQResponseByUserQuery, IEnumerable<ParqResponseRecord>>
{
    public async Task<Result<IEnumerable<ParqResponseRecord>>> Handle(GetParQResponseByUserQuery request, CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql = $"""
                           SELECT
                               r.id AS {nameof(ParqResponseRecord.Id)},
                               r.question_id AS {nameof(ParqResponseRecord.QuestionId)},
                               q.question AS {nameof(ParqResponseRecord.Question)},
                               r.answer AS {nameof(ParqResponseRecord.Answer)}
                           FROM users.parq_responses r
                           INNER JOIN users.parq_questions q ON r.question_id = q.id
                           WHERE r.user_id = @UserId
                           """;

        var parameters = new { request.UserId };

        IEnumerable<ParqResponseRecord> responses = await connection.QueryAsync<ParqResponseRecord>(sql, parameters);

        return Result.Success(responses);
    }
}
