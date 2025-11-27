using Microsoft.EntityFrameworkCore;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class UserRepository(ApplicationDbContext context) : IUserRepository
{
    public async Task<ApplicationUser?> GetByIdAsync(long id)
    {
        return await context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        return await context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task AddAsync(ApplicationUser user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        context.Users.Update(user);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var user = await context.Users.FindAsync(id);
        if (user != null)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}