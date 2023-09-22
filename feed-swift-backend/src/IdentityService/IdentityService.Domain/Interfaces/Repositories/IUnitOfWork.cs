using Microsoft.EntityFrameworkCore.Storage;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUnitOfWork : IAsyncDisposable
{
    Task<IDbContextTransaction> BeginTransactionAsync();
    public Task<bool> CommitAsync();
}