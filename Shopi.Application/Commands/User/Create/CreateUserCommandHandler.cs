using MediatR;
using Shopi.DomainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopi.Application.Commands.User.Create;

public class CreateUserCommandHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = DomainModel.Models.UserEntity.CreateUser(request.name, request.lastName, request.phone);
        await unitOfWork.UserRepository.AddAsync(user, cancellationToken);
        await unitOfWork.CommitAsync(cancellationToken);
    }
}
