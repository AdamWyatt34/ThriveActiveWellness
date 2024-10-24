using MediatR;
using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
