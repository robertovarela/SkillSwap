using RDS.Core.Dtos.SkillSwapRequest;
using RDS.Core.Enums;

namespace RDS.Core.Interfaces;

public interface ISkillSwapRepository
{
    Task<SkillSwapRequestDto?> GetByIdAsync(long id, long professionalId);
    Task<SkillSwapUpdateStatusDto?> GetForEditAsync(long id, long professionalId);
    Task<IEnumerable<SkillSwapRequestDto>> GetByProfessionalIdAsync(long professionalId);
    Task<IEnumerable<SkillSwapRequestDto>> GetAllAsync();
    Task<IEnumerable<SkillSwapRequestDto>> GetAllAdminAsync();
    Task<long> AddAsync(SkillSwapRequestCreateDto dto);
    Task UpdateStatusAsync(long id, StatusSwapRequest status);
    Task MakeCounterOfferAsync(CounterOfferDto dto);
}
