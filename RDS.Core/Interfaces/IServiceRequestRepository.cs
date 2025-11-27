using RDS.Core.Models;

namespace RDS.Core.Interfaces;

public interface IServiceRequestRepository
{
    Task<ServiceRequest?> GetByIdAsync(long id);
    Task<IEnumerable<ServiceRequest>> GetByClientIdAsync(long clientId);
    Task<IEnumerable<ServiceRequest>> GetByServiceIdAsync(long serviceId);
    Task<IEnumerable<ServiceRequest>> GetAllAsync();
    Task AddAsync(ServiceRequest request);
    Task UpdateStatusAsync(long id, string status);
}