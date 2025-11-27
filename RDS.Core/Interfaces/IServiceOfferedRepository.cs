using RDS.Core.Dtos.ServiceOffered;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Core.Interfaces;

public interface IServiceOfferedRepository
{
    Task<ServiceOfferedDto?> GetByIdAsync(long id);
    Task<IEnumerable<ServiceOfferedDto>> GetByIdsAsync(IEnumerable<long> ids);
    Task<ServiceOfferedUpdateDto?> GetForEditAsync(long id, long professionalId);
    Task<IEnumerable<ServiceOfferedDto>> GetByCreatedDateAsync(DateTimeOffset date);
    Task<IEnumerable<ServiceOfferedDto>> GetAllAsync();
    Task<IEnumerable<ServiceOfferedDto>> GetByProfessionalIdAsync(long professionalId);
    Task<IEnumerable<ServiceOfferedDto>> GetWithoutProfessionalIdAsync(long professionalId);
    Task<long> AddAsync(ServiceOfferedCreateDto service);
    Task UpdateAsync(ServiceOfferedUpdateDto service);
    Task DeleteAsync(long id);
    Task<PaginatedResult<ServiceOfferedDto>> GetPagedAsync(FilterParams filter, long professionalId, bool excludeProfessional);
}