using RDS.Core.Models;

namespace RDS.Core.Interfaces;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(long id);
    Task<Review?> GetByServiceRequestIdAsync(long serviceRequestId);
    Task AddAsync(Review review);
}