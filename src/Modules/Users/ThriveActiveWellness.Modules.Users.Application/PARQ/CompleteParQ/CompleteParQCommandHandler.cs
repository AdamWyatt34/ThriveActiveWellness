using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Domain.PARQ;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Application.PARQ.CompleteParQ;

internal sealed class CompleteParQCommandHandler(IUserRepository userRepository, IParQCompletionRepository parQCompletionRepository) : ICommandHandler<CompleteParQCommand>
{
    public async Task<Result> Handle(CompleteParQCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);
        if (user == null)
        {
            return Result.Failure<Result>(UserErrors.NotFound(request.UserId));
        }

        var completion = ParQCompletion.Create(request.UserId);
        parQCompletionRepository.Insert(completion);
        
        return Result.Success();
    }
}
