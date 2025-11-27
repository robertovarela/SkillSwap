using RDS.Core.Models;

namespace RDS.Core.Libraries.Services;

public interface IReviewService
{
    Task<IEnumerable<Review>> GetByServiceIdAsync(long serviceId);
    Task<Review?> GetByIdAsync(long id);
    Task AddAsync(Review review);
    Task DeleteAsync(long id);
}