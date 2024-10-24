using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Exercises.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Exercises.Domain.Clients;

namespace ThriveActiveWellness.Modules.Exercises.Application.Clients.CreateClient;

internal sealed class CreateClientCommandHandler(IClientRepository clientRepository, IUnitOfWork unitOfWork) 
    : ICommandHandler<CreateClientCommand>
{
    public async Task<Result> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = Client.Create(request.ClientId, request.FirstName, request.LastName, request.Email);

        clientRepository.Insert(client);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
