using IdentityService.Domain.Entities;
using IdentityService.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IdentityService.Persistence.Repositories;

public sealed class UserRepository: BaseRepository<User>,IUserRepository
{
    public UserRepository(DbContext context) : base(context)
    {
    }

    public Task<User?> GetByEmailAsync(string email) => DbSet.SingleOrDefaultAsync(x => x.Email == email);
}