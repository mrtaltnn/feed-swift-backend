using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUserRepository: IRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}