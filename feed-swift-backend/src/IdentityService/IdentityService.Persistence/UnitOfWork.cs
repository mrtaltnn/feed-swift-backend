using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace IdentityService.Persistence;

public sealed class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ILogger<UnitOfWork<TContext>> _logger;

    public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger)
    {
        _context = context;
        _logger = logger;
    }
    
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
    
    public async Task<bool> CommitAsync(bool useTransaction = true)
    {
        await using var transaction = await BeginTransactionAsync();
        try
        {
            await _context.SaveChangesAsync();
            if(useTransaction)
                await transaction.CommitAsync();
            return true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            _logger.LogError(dbUpdateException, "An error occured during commiting changes");
            if(useTransaction)
                await transaction.RollbackAsync();
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An error occured during saving changes");
        }
        return false;
    }

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}