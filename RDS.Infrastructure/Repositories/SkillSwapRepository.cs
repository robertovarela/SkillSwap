using Microsoft.EntityFrameworkCore;
using RDS.Core.Dtos.SkillSwapRequest;
using RDS.Core.Enums;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class SkillSwapRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ISkillSwapRepository
{
    public async Task<SkillSwapRequestDto?> GetByIdAsync(long id, long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.SkillSwapRequests
            .AsNoTracking()
            .Where(s => s.Id == id && (s.ProfessionalBId == professionalId || s.ProfessionalAId == professionalId))
            .Select(s => new SkillSwapRequestDto
            {
                Id = s.Id,
                ProfessionalBId = s.ProfessionalBId,
                ProfessionalAId = s.ProfessionalAId,
                ServiceBId = s.ServiceBId,
                ServiceAId = s.ServiceAId,
                Status = s.Status,
                OfferedAmount = s.OfferedAmount,
                SwapDate = s.SwapDate,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<SkillSwapUpdateStatusDto?> GetForEditAsync(long id, long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.SkillSwapRequests
            .AsNoTracking()
            .Where(s => s.Id == id && (s.ProfessionalBId == professionalId || s.ProfessionalAId == professionalId))
            .Select(s => new SkillSwapUpdateStatusDto
            {
                Id = s.Id,
                Status = s.Status,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<SkillSwapRequestDto>> GetByProfessionalIdAsync(long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.SkillSwapRequests
            .AsNoTracking()
            .Where(s => s.ProfessionalBId == professionalId || s.ProfessionalAId == professionalId)
            .Select(s => new SkillSwapRequestDto
            {
                Id = s.Id,
                ProfessionalBId = s.ProfessionalBId,
                ProfessionalAId = s.ProfessionalAId,
                ServiceBId = s.ServiceBId,
                ServiceAId = s.ServiceAId,
                Status = s.Status,
                SwapDate = s.SwapDate,
                OfferedAmount = s.OfferedAmount,
                ProfessionalAName = s.ProfessionalA!.ProfessionalName,
                ProfessionalBName = s.ProfessionalB!.ProfessionalName,
                ServiceADescription = s.ServiceA!.Description,
                ServiceBDescription = s.ServiceB!.Description,
                ServiceATitle = s.ServiceA.Title,
                ServiceBTitle = s.ServiceB.Title
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SkillSwapRequestDto>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.SkillSwapRequests
            .AsNoTracking()
            .Select(s => new SkillSwapRequestDto
            {
                Id = s.Id,
                ProfessionalBId = s.ProfessionalBId,
                ProfessionalAId = s.ProfessionalAId,
                ServiceBId = s.ServiceBId,
                ServiceAId = s.ServiceAId,
                Status = s.Status,
                SwapDate = s.SwapDate,
                OfferedAmount = s.OfferedAmount
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<SkillSwapRequestDto>> GetAllAdminAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.SkillSwapRequests
            .AsNoTracking()
            .OrderByDescending(s => s.SwapDate)
            .Select(s => new SkillSwapRequestDto
            {
                Id = s.Id,
                Status = s.Status,
                SwapDate = s.SwapDate,
                ProfessionalBId = s.ProfessionalBId,
                ProfessionalAId = s.ProfessionalAId,
                ProfessionalAName = s.ProfessionalA!.ProfessionalName,
                ProfessionalBName = s.ProfessionalB!.ProfessionalName,
                ServiceATitle = s.ServiceA!.Title,
                ServiceBTitle = s.ServiceB!.Title,
                ServiceADescription = s.ServiceA.Description,
                ServiceBDescription = s.ServiceB != null ? s.ServiceB.Description : null,
                OfferedAmount = s.OfferedAmount
            })
            .ToListAsync();
    }

    public async Task<long> AddAsync(SkillSwapRequestCreateDto dto)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = new SkillSwapRequest
        {
            ProfessionalBId = dto.ProfessionalBId,
            ProfessionalAId = dto.ProfessionalAId,
            ServiceBId = dto.ServiceBId,
            ServiceAId = dto.ServiceAId,
            SwapDate = dto.SwapDate,
            Status = dto.Status,
            OfferedAmount = dto.OfferedAmount
        };
        context.SkillSwapRequests.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateStatusAsync(long id, StatusSwapRequest status)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existing = await context.SkillSwapRequests
            .FirstOrDefaultAsync(s => s.Id == id);
        if (existing == null)
            throw new KeyNotFoundException("SkillSwap não encontrada");

        existing.Status = status;

        await context.SaveChangesAsync();
    }

    public async Task MakeCounterOfferAsync(CounterOfferDto dto)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existing = await context.SkillSwapRequests
            .FirstOrDefaultAsync(s => s.Id == dto.SkillSwapRequestId);
        if (existing == null)
            throw new KeyNotFoundException("SkillSwap não encontrada");

        existing.Status = StatusSwapRequest.Countered;
        existing.ServiceAId = dto.NewServiceAId;
        existing.OfferedAmount = dto.NewOfferedAmount;

        await context.SaveChangesAsync();
    }
}
