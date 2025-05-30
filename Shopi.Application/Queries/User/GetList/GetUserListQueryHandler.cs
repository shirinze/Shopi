using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Shopi.Application.Features;
using Shopi.Application.Helpers;
using Shopi.Application.ViewModels;
using Shopi.DomainService;
using Shopi.DomainService.Specifications;
using System.Text.Json;


namespace Shopi.Application.Queries.User.GetList;

public class GetUserListQueryHandler(IUnitOfWork unitOfWork,IMemoryCache memoryCache) : IRequestHandler<GetUserListQuery, PaginationResult<UserViewModel>>
{
    public async Task<PaginationResult<UserViewModel>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        var key = $"UserList:{JsonSerializer.Serialize(request)}";
        var paginationResult = memoryCache.Get<PaginationResult<UserViewModel>>(key);
        if(paginationResult is null)
        {
            var specification = new GetUserByFiltersSpecification(request.Q, request.OrderType, request.PageSize, request.PageNumber);
            var (totalCount, users) = await unitOfWork.UserRepository.GetListAsync(specification, cancellationToken);

            var viewmodels = users.ToViewModel();
            paginationResult = PaginationResult<UserViewModel>.Create(request.PageSize ?? 0, request.PageSize?? 0, totalCount, viewmodels);

            memoryCache.Set(key, paginationResult,TimeSpan.FromSeconds(30));
        }
        return paginationResult;
    }
}
