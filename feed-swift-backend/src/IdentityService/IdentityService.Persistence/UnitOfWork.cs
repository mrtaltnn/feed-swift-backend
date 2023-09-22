using IdentityService.Domain.Interfaces.Repositories;
using IdentityService.Persistence.Database;
using IdentityService.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace IdentityService.Persistence;

public sealed class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private readonly ILogger<UnitOfWork<TContext>> _logger;

    public UnitOfWork(TContext context, ILogger<UnitOfWork<TContext>> logger,IUserRepository userRepository)
    {
        _context = context;
        _logger = logger;
        Users = userRepository;
    }
    
    public IUserRepository Users { get; }
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await _context.Database.BeginTransactionAsync();
    
    public async Task<bool> CommitAsync()
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.SaveChangesAsync();
            await transaction.CommitAsync();
            return true;
        }
        catch (DbUpdateException dbUpdateException)
        {
            _logger.LogError(dbUpdateException, "An error occured during commiting changes");
            await transaction.RollbackAsync();
            return false;
        }
    }

    public async ValueTask DisposeAsync() => await _context.DisposeAsync();
}