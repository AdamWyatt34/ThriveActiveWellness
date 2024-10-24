using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Application.Users.UpdateUser;

internal sealed class UpdateUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
    : ICommandHandler<UpdateUserCommand>
{
    public async Task<Result> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        User? user = await userRepository.GetAsync(request.UserId, cancellationToken);

        if (user is null)
        {
            return Result.Failure(UserErrors.NotFound(request.UserId));
        }

        user.Update(request.FirstName, request.LastName, request.Email);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
