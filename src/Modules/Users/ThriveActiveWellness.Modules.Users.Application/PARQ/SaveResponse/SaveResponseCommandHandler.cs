using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.SaveResponse;

internal sealed class SaveResponseCommandHandler(IParQResponseRepository parQResponseRepository, IUnitOfWork unitOfWork) : ICommandHandler<SaveResponseCommand>
{
    public async Task<Result> Handle(SaveResponseCommand request, CancellationToken cancellationToken)
    {
        var response = ParqResponse.Create(request.UserId, request.QuestionId, request.Answer);

        parQResponseRepository.Insert(response);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
