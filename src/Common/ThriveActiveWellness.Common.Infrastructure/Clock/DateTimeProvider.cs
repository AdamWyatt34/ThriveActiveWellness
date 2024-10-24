using ThriveActiveWellness.Common.Application.Clock;

namespace ThriveActiveWellness.Common.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
