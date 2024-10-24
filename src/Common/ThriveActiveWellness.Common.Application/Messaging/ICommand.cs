using MediatR;
using ThriveActiveWellness.Common.Domain;

namespace ThriveActiveWellness.Common.Application.Messaging;

public interface ICommand : IRequest<Result>, IBaseCommand;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
