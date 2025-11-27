using RDS.Core.Dtos.ProfessionalProfile;

namespace RDS.Core.Interfaces;

public interface IProfessionalProfileRepository
{
    Task<IEnumerable<ProfessionalProfileDto>> GetAllAsync();
    Task<ProfessionalProfileDto?> GetByIdAsync(long id);
    Task<IEnumerable<ProfessionalProfileDto>> GetByIdsAsync(IEnumerable<long> ids);
    Task<ProfessionalProfileUpdateDto?> GetForEditingAsync(long id);
    Task<ProfessionalProfileDto?> GetByUserIdAsync(long userId);
    Task<IEnumerable<ProfessionalProfileDto>> SearchByNameAsync(string search, long? exceptUserId = null);
    Task<IEnumerable<ProfessionalProfileDto>> GetProfilesWithMessagesAsync(long userId);
    Task AddAsync(ProfessionalProfileCreateDto dto);
    Task UpdateAsync(ProfessionalProfileUpdateDto updated);
    Task DeleteAsync(long id);
    Task UpdateBalanceAsync(long profileId, decimal newBalance);
}
