using Shopi.DomainService.Repositories;

namespace Shopi.DomainService;

public interface IUnitOfWork
{
    public Task CommitAsync(CancellationToken cancellationToken);
    public IUserRepository UserRepository { get; init; }
}
