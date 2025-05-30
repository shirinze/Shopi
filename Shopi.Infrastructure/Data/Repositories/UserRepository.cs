using Microsoft.EntityFrameworkCore;
using Shopi.DomainModel.Models;
using Shopi.DomainService.BaseSpecifications;
using Shopi.DomainService.Repositories;
using Shopi.Infrastructure.Data.DbContexts;
using Shopi.Infrastructure.Helpers;

namespace Shopi.Infrastructure.Data.Repositories;

public class UserRepository(ShopiDbContext db) : IUserRepository
{
    private readonly DbSet<UserEntity> set = db.Set<UserEntity>();

    public async Task AddAsync(UserEntity user, CancellationToken cancellationToken)
    {
        user.Create();
        await set.AddAsync(user, cancellationToken);
    }

    public void Delete(UserEntity user)
    {
        user.Delete();
        set.Update(user);
    }

    public async Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await set.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id,cancellationToken);
    }

    public async Task<(int, List<UserEntity>)> GetListAsync(BaseSpecification<UserEntity> specification, CancellationToken cancellationToken)
    {
        var query = set.AsNoTracking().Specify(specification);
        var totalCount = await query.CountAsync(cancellationToken);

        if (specification.IsPaginationEnabled)
        {
            query = query.Skip(specification.Skip).Take(specification.Take);
        }

        var result = await query.ToListAsync(cancellationToken);

        return (totalCount, result);
    }

    public void Update(UserEntity user)
    {
        user.Update();
        set.Update(user);
    }
}
