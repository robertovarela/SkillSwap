using Microsoft.EntityFrameworkCore;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Persistence;

namespace RDS.Infrastructure.Services;

public class ReviewService(ApplicationDbContext context) : IReviewService
{
    public async Task<IEnumerable<Review>> GetByServiceIdAsync(long serviceId)
        => await context.Reviews
            .Where(r => r.ServiceId == serviceId)
            .Include(r => r.Reviewer)
            .AsNoTracking()
            .ToListAsync();

    public async Task<Review?> GetByIdAsync(long id)
        => await context.Reviews.FindAsync(id);

    public async Task AddAsync(Review review)
    {
        context.Reviews.Add(review);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        var rev = await context.Reviews.FindAsync(id);
        if (rev != null)
        {
            context.Reviews.Remove(rev);
            await context.SaveChangesAsync();
        }
    }
}