using System.Data.Common;
using Dapper;
using ThriveActiveWellness.Common.Application.Authorization;
using ThriveActiveWellness.Common.Application.Data;
using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Application.Users.GetUserPermissions;

internal sealed class GetUserPermissionsQueryHandler(IDbConnectionFactory dbConnectionFactory)
    : IQueryHandler<GetUserPermissionsQuery, PermissionsResponse>
{
    public async Task<Result<PermissionsResponse>> Handle(
        GetUserPermissionsQuery request,
        CancellationToken cancellationToken)
    {
        await using DbConnection connection = await dbConnectionFactory.OpenConnectionAsync();

        const string sql =
            $"""
             SELECT DISTINCT
                 u.id AS {nameof(UserPermission.UserId)},
                 rp.permission_code AS {nameof(UserPermission.Permission)}
             FROM users.users u
             JOIN users.user_roles ur ON ur.user_table_id = u.table_id
             JOIN users.role_permissions rp ON rp.role_name = ur.role_name
             WHERE u.identity_id = @IdentityId
             """;

        List<UserPermission> permissions = (await connection.QueryAsync<UserPermission>(sql, request)).AsList();

        if (!permissions.Any())
        {
            return Result.Failure<PermissionsResponse>(UserErrors.NotFound(request.IdentityId));
        }

        return new PermissionsResponse(permissions[0].UserId, permissions.Select(p => p.Permission).ToHashSet());
    }

    internal sealed class UserPermission
    {
        internal Guid UserId { get; init; }

        internal string Permission { get; init; }
    }
}
