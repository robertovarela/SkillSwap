using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;

namespace RDS.Infrastructure.Services;

/// <summary>
/// Implementação do serviço de gerenciamento de solicitações de serviços.
/// </summary>
public class ServiceRequestService(IServiceRequestRepository repository) : IServiceRequestService
{
    public async Task<ServiceRequest?> GetByIdAsync(long id)
    {
        return await repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ServiceRequest>> GetByClientIdAsync(long clientId)
    {
        return await repository.GetByClientIdAsync(clientId);
    }

    public async Task<IEnumerable<ServiceRequest>> GetByServiceIdAsync(long serviceId)
    {
        return await repository.GetByServiceIdAsync(serviceId);
    }

    public async Task<IEnumerable<ServiceRequest>> GetAllAsync()
    {
        // Aqui você pode buscar todas as solicitações
        return await repository.GetAllAsync();
    }

    public async Task AddAsync(ServiceRequest request)
    {
        await repository.AddAsync(request);
    }

    public async Task UpdateStatusAsync(long requestId, string status)
    {
        await repository.UpdateStatusAsync(requestId, status);
    }
}