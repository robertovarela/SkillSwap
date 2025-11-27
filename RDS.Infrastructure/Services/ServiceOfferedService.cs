using RDS.Core.Dtos.ServiceOffered;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Infrastructure.Services;

/// <summary>
/// Implementação do serviço de gerenciamento de serviços profissionais.
/// </summary>
public class ServiceOfferedService(IServiceOfferedRepository repo) : IServiceOfferedService
{
    public async Task<ServiceOfferedDto?> GetByIdAsync(long id)
    {
        return await repo.GetByIdAsync(id);
    }

    public Task<IEnumerable<ServiceOfferedDto>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return repo.GetByIdsAsync(ids);
    }

    public async Task<ServiceOfferedUpdateDto?> GetForEditAsync(long id, long professionalId)
    {
        return await repo.GetForEditAsync(id, professionalId);
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetByCreatedDateAsync(DateTimeOffset date)
    {
        return await repo.GetByCreatedDateAsync(date);
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetAllAsync()
    {
        return await repo.GetAllAsync();
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetByProfessionalIdAsync(long professionalId)
    {
        return await repo.GetByProfessionalIdAsync(professionalId);
    }

    public Task<IEnumerable<ServiceOfferedDto>> GetWithoutProfessionalIdAsync(long professionalId)
    {
        return repo.GetWithoutProfessionalIdAsync(professionalId);
    }

    public async Task<long> AddAsync(ServiceOfferedCreateDto dto)
    {
        var entity = new ServiceOffered
        {
            ProfessionalProfileId = dto.ProfessionalProfileId,
            CategoryId = dto.CategoryId,
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            IsActive = dto.IsActive,
            CreatedAt = DateTimeOffset.UtcNow
        };
        await repo.AddAsync(dto);
        return entity.Id;
    }

    public async Task UpdateAsync(ServiceOfferedUpdateDto service)
    {
        await repo.UpdateAsync(service);
    }

    public async Task DeleteAsync(long id)
    {
        await repo.DeleteAsync(id);
    }

    public async Task<PaginatedResult<ServiceOfferedDto>> GetPagedAsync(FilterParams filter, long professionalId, bool excludeProfessional)
    {
        return await repo.GetPagedAsync(filter, professionalId, excludeProfessional);
    }
}