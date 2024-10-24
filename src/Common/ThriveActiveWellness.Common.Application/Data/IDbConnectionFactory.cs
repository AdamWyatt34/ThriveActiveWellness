using System.Data.Common;

namespace ThriveActiveWellness.Common.Application.Data;

public interface IDbConnectionFactory
{
    ValueTask<DbConnection> OpenConnectionAsync();
}
