using Shopi.DomainModel.Models;
using Shopi.DomainService.BaseSpecifications;
using System;

namespace Shopi.DomainService.Repositories;

public interface IUserRepository
{
    public Task AddAsync(UserEntity user, CancellationToken cancellationToken);
    public void Update(UserEntity user);
    public void Delete(UserEntity user);
    public Task<UserEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    public Task<(int, List<UserEntity>)> GetListAsync(BaseSpecification<UserEntity> specification, CancellationToken cancellationToken);
}
