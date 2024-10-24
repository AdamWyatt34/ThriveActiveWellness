using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Common.Application.Exceptions;

public sealed class ThriveActiveWellnessException : Exception
{
    public ThriveActiveWellnessException(string requestName, Error? error = default, Exception? innerException = default)
        : base("Application exception", innerException)
    {
        RequestName = requestName;
        Error = error;
    }

    public string RequestName { get; }

    public Error? Error { get; }
}
