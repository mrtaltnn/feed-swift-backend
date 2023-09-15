using System.Linq.Expressions;
using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces;
using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.Repositories;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DbContext _dbContext;
    protected readonly DbSet<TEntity> DbSet;
    
    protected BaseRepository(DbContext context)
    {
        _dbContext = context;
        DbSet = _dbContext.Set<TEntity>();
    }
    
    public IEnumerable<TEntity> Get()
    {
        return DbSet.ToList();
    }

    public async Task<IEnumerable<TEntity>> GetAsync()
    {
        return await DbSet.ToListAsync();
    }

    public IEnumerable<TEntity> GetAsNoTracking()
    {
        return DbSet.AsNoTracking().ToList();
    }
    

    public async Task<TEntity> GetByIdAsync(object id)
    {
        return await DbSet.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public TEntity? GetByIdAsNoTracking(object id)
    {
        var entity = DbSet.Find(id);
        if (entity == null) return null;
        _dbContext.Entry(entity).State = EntityState.Detached;
        return entity;
    }

    public IQueryable<TEntity> FindBy()
    {
        return DbSet;
    }
    public IQueryable<TEntity> FindByAsNoTracking()
    {
        return DbSet.AsNoTracking();
    }

    public async Task<int> InsertAsync(TEntity entity)
    {
        return (await DbSet.AddAsync(entity)).Entity.Id;
    }

    public async Task InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.AddRangeAsync(entities);
    }

    public void Update(TEntity entityToUpdate)
    {
        DbSet.Update(entityToUpdate);
    }

    public void UpdateRange(IEnumerable<TEntity> entitiesToUpdate)
    {
        DbSet.UpdateRange(entitiesToUpdate);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await DbSet.AnyAsync(entity => entity.Id == id);
    }

    public void Delete(object id, bool hardDelete = false)
    {
        var entityToDelete = DbSet.Find(id);
        if (entityToDelete != null) Delete(entityToDelete);
    }

    public void Delete(TEntity entityToDelete, bool hardDelete = false)
    {
        if (hardDelete)
        {
            DbSet.Remove(entityToDelete);
        }
        else
        {
            entityToDelete.Delete();
            DbSet.Update(entityToDelete);
        }
    }

    public void DeleteRange(IEnumerable<TEntity> entitiesToDelete, bool hardDelete = false)
    {
        if (hardDelete)
        {
            DbSet.RemoveRange(entitiesToDelete);
            return;
        }

        foreach (var entity in entitiesToDelete) entity.Delete();
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _dbContext.Dispose();
        }
    }
}