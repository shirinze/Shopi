using MediatR;
using Shopi.Application.Exceptions;
using Shopi.DomainService;
using Shopi.Resources;

namespace Shopi.Application.Commands.User.ToggleActivation;

public class ToggleActivationUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<ToggleActivationUserCommand>
{
    public async Task Handle(ToggleActivationUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.UserEntity),request.Id));
        user.ToggleActivation();
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
