using MediatR;
using Shopi.Application.Exceptions;
using Shopi.DomainService;
using Shopi.Resources;

namespace Shopi.Application.Commands.User.Delete;

public class DeleteUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserCommand>
{
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.UserEntity), request.id));

        unitOfWork.UserRepository.Delete(user);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
