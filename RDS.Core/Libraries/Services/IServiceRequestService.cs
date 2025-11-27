using RDS.Core.Models;

namespace RDS.Core.Libraries.Services;

/// <summary>
/// Interface para operações com solicitações de serviços feitas por clientes.
/// </summary>
public interface IServiceRequestService
{
    Task<ServiceRequest?> GetByIdAsync(long id);
    Task<IEnumerable<ServiceRequest>> GetByClientIdAsync(long clientId);
    Task<IEnumerable<ServiceRequest>> GetByServiceIdAsync(long serviceId);
    Task<IEnumerable<ServiceRequest>> GetAllAsync();
    Task AddAsync(ServiceRequest request);
    Task UpdateStatusAsync(long requestId, string status);
}