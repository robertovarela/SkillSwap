using Microsoft.EntityFrameworkCore;
using RDS.Core.Dtos.ProfessionalProfile;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Persistence;
using RDS.Core.Utils;

namespace RDS.Infrastructure.Repositories;

public class ProfessionalProfileRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IProfessionalProfileRepository
{
    public async Task<IEnumerable<ProfessionalProfileDto>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var profiles = await context.ProfessionalProfiles
            .AsNoTracking()
            .Select(p => new ProfessionalProfileDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Bio = p.Bio,
                Expertise = p.Expertise,
                IsPremium = p.IsPremium,
                ProfessionalName = p.ProfessionalName,
                SkillDolarBalance = p.SkillDolarBalance,
                AcademicRegistry = p.AcademicRegistry,
                Cpf = p.Cpf.HasValue ? p.Cpf.Value.ToFormattedString() : string.Empty,
                BirthDate = p.BirthDate,
                TeachingInstitution = p.TeachingInstitution
            })
            .ToListAsync();

        var profileIdsWithServices = await context.ServiceOffered
            .Select(s => s.ProfessionalProfileId)
            .Distinct()
            .ToListAsync();
            
        var idSet = new HashSet<long>(profileIdsWithServices);
        foreach (var profile in profiles)
        {
            profile.HasAssociatedServices = idSet.Contains(profile.Id);
        }

        return profiles;
    }

    public async Task<ProfessionalProfileDto?> GetByIdAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var profile = await context.ProfessionalProfiles
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new ProfessionalProfileDto
            {
                Id = s.Id,
                UserId = s.UserId,
                Bio = s.Bio,
                Expertise = s.Expertise,
                IsPremium = s.IsPremium,
                ProfessionalName = s.ProfessionalName,
                SkillDolarBalance = s.SkillDolarBalance,
                AcademicRegistry = s.AcademicRegistry,
                Cpf = s.Cpf.HasValue ? s.Cpf.Value.ToFormattedString() : string.Empty,
                BirthDate = s.BirthDate,
                TeachingInstitution = s.TeachingInstitution
            })
            .FirstOrDefaultAsync();

        if (profile is not null)
        {
            profile.HasAssociatedServices = await context.ServiceOffered.AnyAsync(s => s.ProfessionalProfileId == id);
        }

        return profile;
    }

    public async Task<IEnumerable<ProfessionalProfileDto>> GetByIdsAsync(IEnumerable<long> ids)
    {
        var idList = ids?.Distinct().ToList() ?? [];
        if (!idList.Any())
            return [];

        await using var context = await contextFactory.CreateDbContextAsync();
        var profiles = await context.ProfessionalProfiles
            .AsNoTracking()
            .Where(p => idList.Contains(p.Id))
            .Select(p => new ProfessionalProfileDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Bio = p.Bio,
                Expertise = p.Expertise,
                IsPremium = p.IsPremium,
                ProfessionalName = p.ProfessionalName,
                SkillDolarBalance = p.SkillDolarBalance,
                AcademicRegistry = p.AcademicRegistry,
                Cpf = p.Cpf.HasValue ? p.Cpf.Value.ToFormattedString() : string.Empty,
                BirthDate = p.BirthDate,
                TeachingInstitution = p.TeachingInstitution
            })
            .ToListAsync();
            
        var profileIdsWithServices = await context.ServiceOffered
            .Where(s => idList.Contains(s.ProfessionalProfileId))
            .Select(s => s.ProfessionalProfileId)
            .Distinct()
            .ToListAsync();
            
        var idSet = new HashSet<long>(profileIdsWithServices);
        foreach (var profile in profiles)
        {
            profile.HasAssociatedServices = idSet.Contains(profile.Id);
        }

        return profiles;
    }

    public async Task<ProfessionalProfileUpdateDto?> GetForEditingAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ProfessionalProfiles
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new ProfessionalProfileUpdateDto
            {
                Id = s.Id,
                UserId = s.UserId,
                Bio = s.Bio,
                Expertise = s.Expertise,
                IsPremium = s.IsPremium,
                ProfessionalName = s.ProfessionalName,
                AcademicRegistry = s.AcademicRegistry,
                Cpf = s.Cpf.HasValue ? s.Cpf.Value.Number : string.Empty, // Send raw digits for editing
                BirthDate = s.BirthDate,
                TeachingInstitution = s.TeachingInstitution
            })
            .FirstOrDefaultAsync();
    }

    public async Task<ProfessionalProfileDto?> GetByUserIdAsync(long userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var profile = await context.ProfessionalProfiles
            .AsNoTracking()
            .Where(s => s.UserId == userId)
            .Select(s => new ProfessionalProfileDto
            {
                Id = s.Id,
                UserId = s.UserId,
                Bio = s.Bio,
                Expertise = s.Expertise,
                IsPremium = s.IsPremium,
                ProfessionalName = s.ProfessionalName,
                SkillDolarBalance = s.SkillDolarBalance,
                AcademicRegistry = s.AcademicRegistry,
                Cpf = s.Cpf.HasValue ? s.Cpf.Value.ToFormattedString() : string.Empty,
                BirthDate = s.BirthDate,
                TeachingInstitution = s.TeachingInstitution
            })
            .FirstOrDefaultAsync();
            
        if (profile is not null)
        {
            profile.HasAssociatedServices = await context.ServiceOffered.AnyAsync(s => s.ProfessionalProfileId == profile.Id);
        }

        return profile;
    }

    public async Task<IEnumerable<ProfessionalProfileDto>> SearchByNameAsync(string search, long? exceptUserId = null)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.ProfessionalProfiles.AsNoTracking()
            .Where(p => p.ProfessionalName.Contains(search));

        if (exceptUserId.HasValue)
            query = query.Where(p => p.UserId != exceptUserId.Value);

        return await query
            .Select(p => new ProfessionalProfileDto
            {
                Id = p.Id,
                UserId = p.UserId,
                ProfessionalName = p.ProfessionalName,
                SkillDolarBalance = p.SkillDolarBalance
            })
            .Take(10)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProfessionalProfileDto>> GetProfilesWithMessagesAsync(long userId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var relatedUserIds = await context.Messages
            .AsNoTracking()
            .Where(m => m.SenderId == userId || m.ReceiverId == userId)
            .Select(m => m.SenderId == userId ? m.ReceiverId : m.SenderId)
            .Distinct()
            .ToListAsync();

        var profiles = await context.ProfessionalProfiles
            .AsNoTracking()
            .Where(p => relatedUserIds.Contains(p.UserId))
            .OrderBy(p => p.ProfessionalName)
            .Select(p => new ProfessionalProfileDto
            {
                Id = p.Id,
                UserId = p.UserId,
                Bio = p.Bio,
                Expertise = p.Expertise,
                IsPremium = p.IsPremium,
                ProfessionalName = p.ProfessionalName,
                SkillDolarBalance = p.SkillDolarBalance,
                AcademicRegistry = p.AcademicRegistry,
                Cpf = p.Cpf.HasValue ? p.Cpf.Value.ToFormattedString() : string.Empty,
                BirthDate = p.BirthDate,
                TeachingInstitution = p.TeachingInstitution
            })
            .ToListAsync();
            
        var profileIdsOnPage = profiles.Select(p => p.Id).ToList();
        var profileIdsWithServices = await context.ServiceOffered
            .Where(s => profileIdsOnPage.Contains(s.ProfessionalProfileId))
            .Select(s => s.ProfessionalProfileId)
            .Distinct()
            .ToListAsync();
            
        var idSet = new HashSet<long>(profileIdsWithServices);
        foreach (var profile in profiles)
        {
            profile.HasAssociatedServices = idSet.Contains(profile.Id);
        }

        return profiles;
    }

    public async Task AddAsync(ProfessionalProfileCreateDto dto)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = new ProfessionalProfile
        {
            UserId = dto.UserId,
            Bio = dto.Bio,
            Expertise = dto.Expertise,
            IsPremium = dto.IsPremium,
            ProfessionalName = dto.ProfessionalName,
            SkillDolarBalance = ConfigurationCore.InitialSkillDolar,
            AcademicRegistry = dto.AcademicRegistry,
            Cpf = Cpf.Parse(dto.Cpf),
            BirthDate = dto.BirthDate,
            TeachingInstitution = dto.TeachingInstitution
        };
        context.ProfessionalProfiles.Add(entity);
        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(ProfessionalProfileUpdateDto updated)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existing = await context.ProfessionalProfiles
            .FirstOrDefaultAsync(p => p.Id == updated.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Professional profile with ID {updated.Id} not found.");
        }

        existing.UserId = updated.UserId;
        existing.Bio = updated.Bio;
        existing.Expertise = updated.Expertise;
        existing.IsPremium = updated.IsPremium;
        existing.ProfessionalName = updated.ProfessionalName;
        existing.AcademicRegistry = updated.AcademicRegistry;
        existing.Cpf = Cpf.Parse(updated.Cpf);
        existing.BirthDate = updated.BirthDate;
        existing.TeachingInstitution = updated.TeachingInstitution;

        context.ProfessionalProfiles.Update(existing);
        await context.SaveChangesAsync();
    }

    public async Task UpdateBalanceAsync(long profileId, decimal newBalance)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var profile = await context.ProfessionalProfiles.FindAsync(profileId);
        if (profile != null)
        {
            profile.SkillDolarBalance = newBalance;
            await context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var hasServices = await context.ServiceOffered.AnyAsync(s => s.ProfessionalProfileId == id);
        if (hasServices)
        {
            throw new InvalidOperationException("Não é possível excluir um perfil que possui serviços associados.");
        }
        
        var profile = await context.ProfessionalProfiles.FindAsync(id);
        if (profile != null)
        {
            context.ProfessionalProfiles.Remove(profile);
            await context.SaveChangesAsync();
        }
    }
}
