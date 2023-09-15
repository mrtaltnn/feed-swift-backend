using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IRepository<TEntity>: IDisposable where TEntity :BaseEntity
{
    IEnumerable<TEntity> Get();
    Task<IEnumerable<TEntity>> GetAsync();
    IEnumerable<TEntity> GetAsNoTracking();
    Task<TEntity> GetByIdAsync(object id);
    TEntity? GetByIdAsNoTracking(object id);
    IQueryable<TEntity> FindBy();
    IQueryable<TEntity> FindByAsNoTracking();
    Task<int> InsertAsync(TEntity entity);
    Task InsertRangeAsync(IEnumerable<TEntity> entities);
    void Update(TEntity entityToUpdate);
    void UpdateRange(IEnumerable<TEntity> entitiesToUpdate);
    
    Task<bool> ExistsAsync(int id);
    void Delete(object id, bool hardDelete = false);
    void Delete(TEntity entityToDelete, bool hardDelete = false);
    void DeleteRange(IEnumerable<TEntity> entitiesToDelete, bool hardDelete = false);
}