using Microsoft.EntityFrameworkCore;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class ReviewRepository(ApplicationDbContext context) : IReviewRepository
{
    public async Task<Review?> GetByIdAsync(long id)
    {
        return await context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<Review?> GetByServiceRequestIdAsync(long serviceRequestId)
    {
        return await context.Reviews
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.ServiceId == serviceRequestId);
    }

    public async Task AddAsync(Review review)
    {
        context.Reviews.Add(review);
        await context.SaveChangesAsync();
    }
}
