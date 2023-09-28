using IdentityService.Domain.Entities;

namespace IdentityService.Domain.Interfaces.Repositories;

public interface IUserRepository: IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> IsExistByEmailAsync(string email);
}