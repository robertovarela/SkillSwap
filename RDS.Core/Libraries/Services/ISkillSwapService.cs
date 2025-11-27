using RDS.Core.Dtos.SkillSwapRequest;
using RDS.Core.Enums;
using RDS.Core.Models;

namespace RDS.Core.Libraries.Services;

public interface ISkillSwapService
{
    Task<SkillSwapRequestDto?> GetByIdAsync(long id, long professionalId);
    Task<SkillSwapUpdateStatusDto?> GetForEditAsync(long id, long professionalId);
    Task<IEnumerable<SkillSwapRequestDto>> GetByProfessionalIdAsync(long professionalId);
    Task<IEnumerable<SkillSwapRequestDto>> GetAllAsync();
    Task<IEnumerable<SkillSwapRequestDto>> GetAllAdminAsync();
    Task<long> AddAsync(SkillSwapRequestCreateDto dto);
    Task UpdateStatusAsync(long id, StatusSwapRequest status);
    Task MakeCounterOfferAsync(CounterOfferDto dto);
    Task AcceptOfferAsync(long skillSwapId, long acceptingProfessionalId);
}
