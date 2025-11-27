using RDS.Core.Dtos.ProfessionalProfile;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Core.Requests;

namespace RDS.Infrastructure.Services;

public class ProfessionalProfileService(IProfessionalProfileRepository repo) : IProfessionalProfileService
{
    public async Task<IEnumerable<ProfessionalProfileDto>> GetAllAsync()
    {
        return await repo.GetAllAsync();
    }
    public async Task<IEnumerable<ProfessionalProfileDto>> GetFilteredAsync(ProfessionalProfileFilterParams filters)
    {
        /*var query = context.ProfessionalProfiles
            .Include(p => p.User)
            .Include(p => p.Services)
            .AsQueryable();

        if (filters.IsPremium.HasValue)
            query = query.Where(p => p.IsPremium == filters.IsPremium.Value);

        // ReSharper disable once InvertIf
        if (!string.IsNullOrWhiteSpace(filters.Search))
        {
            var term = filters.Search.Trim().ToLower();
            query = query.Where(p =>
                p.Expertise != null &&
                p.Expertise.ToLower().Contains(term)
            );
        }

        return await query.AsNoTracking().ToListAsync();*/

        return await Task.FromResult(Array.Empty<ProfessionalProfileDto>());
    }

    public async Task<ProfessionalProfileDto?> GetByIdAsync(long id)
    {
        return await repo.GetByIdAsync(id);
    }

    public Task<IEnumerable<ProfessionalProfileDto>> GetByIdsAsync(IEnumerable<long> ids)
    {
        return repo.GetByIdsAsync(ids);
    }

    public Task<ProfessionalProfileUpdateDto?> GetForEditingAsync(long id)
    {
        return repo.GetForEditingAsync(id);
    }

    public async Task<ProfessionalProfileDto?> GetByUserIdAsync(long userId)
    {
        return await repo.GetByUserIdAsync(userId);
    }

    public Task<IEnumerable<ProfessionalProfileDto>> SearchByNameAsync(string search, long? exceptUserId = null)
    {
        return repo.SearchByNameAsync(search, exceptUserId);
    }

    public async Task<IEnumerable<ProfessionalProfileDto>> GetProfilesWithMessagesAsync(long userId)
    {
        return await repo.GetProfilesWithMessagesAsync(userId);
    }

    public async Task AddAsync(ProfessionalProfileCreateDto profile)
    {
        await repo.AddAsync(profile);
    }

    public async Task UpdateAsync(ProfessionalProfileUpdateDto profile)
    {
        await repo.UpdateAsync(profile);
    }
    
    public async Task DeleteAsync(long id)
    {
        await repo.DeleteAsync(id);
    }

    public async Task AddBalanceAsync(long professionalProfileId, decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException("O valor a ser adicionado deve ser positivo.", nameof(amount));
        }

        var profile = await repo.GetByIdAsync(professionalProfileId) 
                      ?? throw new KeyNotFoundException("Perfil profissional não encontrado.");

        var newBalance = profile.SkillDolarBalance + amount;
        
        await repo.UpdateBalanceAsync(professionalProfileId, newBalance);
    }
}
