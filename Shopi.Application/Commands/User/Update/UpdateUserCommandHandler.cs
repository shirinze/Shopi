using MediatR;
using Shopi.Application.Exceptions;
using Shopi.DomainService;
using Shopi.Resources;

namespace Shopi.Application.Commands.User.Update;

public class UpdateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateUserCommand>
{
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await unitOfWork.UserRepository.GetByIdAsync(request.id, cancellationToken) ??
            throw new NotFoundException(string.Format(Messages.NotFound,nameof(DomainModel.Models.UserEntity),request.id));

        user.Update(request.name,request.lastName,request.phone);
        unitOfWork.UserRepository.Update(user);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
