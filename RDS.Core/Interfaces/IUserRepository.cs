using RDS.Core.Models;

namespace RDS.Core.Interfaces;

public interface IUserRepository
{
    Task<ApplicationUser?> GetByIdAsync(long id);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task AddAsync(ApplicationUser user);
    Task UpdateAsync(ApplicationUser user);
    Task DeleteAsync(long id);
}