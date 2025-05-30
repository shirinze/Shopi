using Shopi.DomainService;
using Shopi.DomainService.Repositories;
using Shopi.Infrastructure.Data.DbContexts;

namespace Shopi.Infrastructure.Data;

public class UnitOfWork(ShopiDbContext db,
    IUserRepository userRepository)
    : IUnitOfWork
{
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        await db.SaveChangesAsync(cancellationToken);
    }
    public IUserRepository UserRepository { get; init; } = userRepository;
}
