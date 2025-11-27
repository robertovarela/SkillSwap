using Microsoft.EntityFrameworkCore;
using RDS.Core.Dtos.ServiceOffered;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class ServiceOfferedRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
    : IServiceOfferedRepository

{
    public async Task<ServiceOfferedDto?> GetByIdAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                CategoryName = s.Category!.Name,
                ProfessionalProfileId = s.ProfessionalProfileId,
                ProfessionalName = s.ProfessionalProfile!.ProfessionalName
            })
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetByIdsAsync(IEnumerable<long>? ids)
    {
        var idList = ids?.Distinct().ToList() ?? [];
        if (!idList.Any())
            return [];
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Where(s => idList.Contains(s.Id))
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                ProfessionalProfileId = s.ProfessionalProfileId,
            })
            .ToListAsync();
    }

    public async Task<ServiceOfferedUpdateDto?> GetForEditAsync(long id, long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var service = await context.ServiceOffered
            .AsNoTracking()
            .Where(s => s.Id == id && s.ProfessionalProfileId == professionalId)
            .Select(s => new 
            {
                s.Id,
                s.Title,
                s.Description,
                s.Price,
                s.CategoryId
            })
            .FirstOrDefaultAsync();

        if (service == null)
        {
            return null;
        }

        var hasProposals = await context.SkillSwapRequests
            .AnyAsync(swap => swap.ServiceAId == service.Id || swap.ServiceBId == service.Id);

        return new ServiceOfferedUpdateDto
        {
            Id = service.Id,
            Title = service.Title,
            Description = service.Description,
            Price = service.Price,
            CategoryId = service.CategoryId,
            HasProposals = hasProposals
        };
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetByCreatedDateAsync(DateTimeOffset date)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Where(s => s.CreatedAt.Date == date.Date)
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                ProfessionalProfileId = s.ProfessionalProfileId
                /*CategoryName = s.Category!.Name,*/
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId             = s.CategoryId,
                CategoryName           = s.Category!.Name,
                ProfessionalProfileId  = s.ProfessionalProfileId,
                ProfessionalName       = s.ProfessionalProfile!.ProfessionalName
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetByProfessionalIdAsync(long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Where(s => s.ProfessionalProfileId == professionalId)
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                ProfessionalProfileId = s.ProfessionalProfileId
                /*CategoryName = s.Category!.Name,*/
            })
            .ToListAsync();
    }

    public async Task<IEnumerable<ServiceOfferedDto>> GetWithoutProfessionalIdAsync(long professionalId)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        return await context.ServiceOffered
            .AsNoTracking()
            .Where(s => s.ProfessionalProfileId != professionalId)
            .Select(s => new ServiceOfferedDto
            {
                Id = s.Id,
                Title = s.Title,
                Description = s.Description,
                Price = s.Price,
                CreatedAt = s.CreatedAt,
                CategoryId = s.CategoryId,
                ProfessionalProfileId = s.ProfessionalProfileId
                /*CategoryName = s.Category!.Name,*/
            })
            .ToListAsync();
    }

    public async Task<long> AddAsync(ServiceOfferedCreateDto dto)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = new ServiceOffered
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            IsActive = dto.IsActive,
            CategoryId = dto.CategoryId,
            ProfessionalProfileId = dto.ProfessionalProfileId,
            CreatedAt = DateTimeOffset.UtcNow
        };

        context.ServiceOffered.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }


    public async Task UpdateAsync(ServiceOfferedUpdateDto updated)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        // carga la entidad ya con seguimiento(tracked) (predeterminado)
        var existing = await context.ServiceOffered
            .FirstOrDefaultAsync(s => s.Id == updated.Id);
        if (existing == null)
            throw new KeyNotFoundException("Serviço não encontrado");

        existing.Title = updated.Title;
        existing.Description = updated.Description;
        existing.Price = updated.Price;
        existing.IsActive = updated.IsActive;
        existing.CategoryId = updated.CategoryId;

        await context.SaveChangesAsync();
    }


    public async Task DeleteAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var service = await context.ServiceOffered.FindAsync(id);
        if (service != null)
        {
            context.ServiceOffered.Remove(service);
            await context.SaveChangesAsync();
        }
    }

    public async Task<PaginatedResult<ServiceOfferedDto>> GetPagedAsync(FilterParams filter, long professionalId, bool excludeProfessional)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        
        IQueryable<ServiceOffered> query = context
            .ServiceOffered
            .AsNoTracking();

        // Aplica o filtro principal: incluir ou excluir os serviços do profissional
        query = excludeProfessional 
            ? query.Where(s => s.ProfessionalProfileId != professionalId) 
            : query.Where(s => s.ProfessionalProfileId == professionalId);

        // Busca
        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.ToLower();
            query = query.Where(x =>
                x.Title.ToLower().Contains(search) ||
                x.Description.ToLower().Contains(search)
            );
        }

        if (filter.CategoryId is > 0)
        {
            query = query.Where(s => s.CategoryId == filter.CategoryId.Value);
        }

        // Total antes da paginação
        var total = await query.CountAsync();

        // Ordenação melhorada
        query = ApplyOrdering(query, filter.OrderBy, filter.OrderDir);

        // Paginação e projeção para um tipo anônimo
        var services = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(s => new 
            {
                s.Id,
                s.Title,
                s.Description,
                s.Price,
                s.CreatedAt,
                s.CategoryId,
                s.ProfessionalProfileId,
                CategoryName = s.Category!.Name,
                CategoryDescription = s.Category.Description,
                s.ProfessionalProfile!.ProfessionalName
            })
            .ToListAsync();

        // Obter os IDs dos serviços da página atual
        var serviceIds = services.Select(s => s.Id).ToList();

        // Buscar todos os IDs de serviços que têm propostas, em uma única consulta
        var swapRequests = await context.SkillSwapRequests
            .Where(swap => (swap.ServiceAId.HasValue && serviceIds.Contains(swap.ServiceAId.Value)) || serviceIds.Contains(swap.ServiceBId))
            .Select(swap => new { swap.ServiceAId, swap.ServiceBId })
            .ToListAsync();

        var idsWithProposals = new HashSet<long>();
        foreach (var swap in swapRequests)
        {
            if (swap.ServiceAId.HasValue)
            {
                idsWithProposals.Add(swap.ServiceAId.Value);
            }
            idsWithProposals.Add(swap.ServiceBId);
        }

        // Mapear para DTOs em memória
        var items = services.Select(s => new ServiceOfferedDto
        {
            Id = s.Id,
            Title = s.Title,
            Description = s.Description,
            Price = s.Price,
            CreatedAt = s.CreatedAt,
            CategoryId = s.CategoryId,
            ProfessionalProfileId = s.ProfessionalProfileId,
            CategoryName = s.CategoryName,
            CategoryDescription = s.CategoryDescription,
            ProfessionalName = s.ProfessionalName,
            HasProposals = idsWithProposals.Contains(s.Id)
        }).ToList();

        return new PaginatedResult<ServiceOfferedDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }

    private static IQueryable<ServiceOffered> ApplyOrdering(IQueryable<ServiceOffered> query, string? orderBy, string? orderDir)
    {
        var isDescending = orderDir?.Equals("desc", StringComparison.OrdinalIgnoreCase) ?? false;

        return orderBy?.ToLower() switch
        {
            "title" => isDescending ? query.OrderByDescending(x => x.Title) : query.OrderBy(x => x.Title),
            "createdat" or "date" => isDescending ? query.OrderByDescending(x => x.CreatedAt) : query.OrderBy(x => x.CreatedAt),
            _ => query.OrderByDescending(x => x.CreatedAt) // Padrão: mais recentes primeiro
        };
    }
}