using ThriveActiveWellness.Common.Application.Messaging;
using ThriveActiveWellness.Common.Domain;
using ThriveActiveWellness.Modules.Users.Application.Abstractions.Data;
using ThriveActiveWellness.Modules.Users.Domain.Users;

namespace ThriveActiveWellness.Modules.Users.Application.Users.RegisterUser;

internal sealed class RegisterUserCommandHandler(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : ICommandHandler<RegisterUserCommand, Guid>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = User.Create(
            request.FirstName,
            request.LastName,
            request.Email,
            request.IdentityId,
            Role.Client);

        userRepository.Insert(user);

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}
