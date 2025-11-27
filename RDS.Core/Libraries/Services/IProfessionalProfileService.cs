using RDS.Core.Dtos.ProfessionalProfile;
using RDS.Core.Requests;

namespace RDS.Core.Libraries.Services;

public interface IProfessionalProfileService
{
    Task<IEnumerable<ProfessionalProfileDto>> GetAllAsync();
    Task<IEnumerable<ProfessionalProfileDto>> GetFilteredAsync(ProfessionalProfileFilterParams filters);
    Task<ProfessionalProfileDto?> GetByIdAsync(long id);
    Task<IEnumerable<ProfessionalProfileDto>> GetByIdsAsync(IEnumerable<long> ids);
    Task<ProfessionalProfileUpdateDto?> GetForEditingAsync(long id);
    Task<ProfessionalProfileDto?> GetByUserIdAsync(long userId);
    Task<IEnumerable<ProfessionalProfileDto>> SearchByNameAsync(string search, long? exceptUserId = null);
    Task<IEnumerable<ProfessionalProfileDto>> GetProfilesWithMessagesAsync(long userId);
    Task AddAsync(ProfessionalProfileCreateDto profile);
    Task UpdateAsync(ProfessionalProfileUpdateDto profile);
    Task DeleteAsync(long id);
    Task AddBalanceAsync(long professionalProfileId, decimal amount);
}
