using MediatR;
using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
