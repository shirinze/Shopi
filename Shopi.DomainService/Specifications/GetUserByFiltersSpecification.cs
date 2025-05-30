using Shopi.DomainModel.Models;
using Shopi.DomainService.BaseSpecifications;

namespace Shopi.DomainService.Specifications;

public class GetUserByFiltersSpecification:BaseSpecification<UserEntity>
{
    public GetUserByFiltersSpecification(string? q,OrderType? orderType,int? pageSize,int? pageNumber)
    {
        AddCriteria(x => x.IsActive);

        if (!string.IsNullOrEmpty(q))
        {
            AddCriteria(x => x.Name.Contains(q) || x.LastName.Contains(q));
        }

        if (orderType != null)
        {
            AddOrderBy(x => x.Id, orderType.Value);
        }

        if (pageSize.HasValue && pageNumber.HasValue)
        {
            AddPagination(pageSize.Value, pageNumber.Value);
        }
    }

}
