using MediatR;
using Shopi.Application.Features;
using Shopi.Application.ViewModels;
using Shopi.DomainService.BaseSpecifications;

namespace Shopi.Application.Queries.User.GetList;

public record GetUserListQuery
    (
    string? Q,
    OrderType? OrderType,
    int? PageSize,
    int? PageNumber
    ):IRequest<PaginationResult<UserViewModel>>;

