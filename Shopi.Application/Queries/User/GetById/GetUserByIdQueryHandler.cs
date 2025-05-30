
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Shopi.Application.Exceptions;
using Shopi.Application.Helpers;
using Shopi.Application.ViewModels;
using Shopi.DomainModel.Models;
using Shopi.DomainService;
using Shopi.Resources;

namespace Shopi.Application.Queries.User.GetById;

public class GetUserByIdQueryHandler(IUnitOfWork unitOfWork, IMemoryCache memoryCache ) : IRequestHandler<GetUserByIdQuery, UserViewModel>
{
    public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var viewModel = memoryCache.Get<UserViewModel>(request.id);
        if (viewModel == null)
        {
            var user = await unitOfWork.UserRepository.GetByIdAsync(request.id, cancellationToken) ??
                     throw new NotFoundException(string.Format(Messages.NotFound, nameof(DomainModel.Models.UserEntity), request.id));

            viewModel = user.ToViewModel();
            memoryCache.Set(request.id, viewModel, TimeSpan.FromSeconds(30));
        }
        return viewModel;
     
    }
}
