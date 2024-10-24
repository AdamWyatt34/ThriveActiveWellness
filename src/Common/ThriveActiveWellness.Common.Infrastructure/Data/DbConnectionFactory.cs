using System.Data.Common;
using Npgsql;
using ThriveActiveWellness.Common.Application.Data;

namespace ThriveActiveWellness.Common.Infrastructure.Data;

internal sealed class DbConnectionFactory(NpgsqlDataSource dataSource) : IDbConnectionFactory
{
    public async ValueTask<DbConnection> OpenConnectionAsync()
    {
        return await dataSource.OpenConnectionAsync();
    }
}
