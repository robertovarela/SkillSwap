using Microsoft.EntityFrameworkCore;
using RDS.Core.Dtos.Category;
using RDS.Core.Interfaces;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;
using RDS.Persistence;

namespace RDS.Infrastructure.Repositories;

public class CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ICategoryRepository
{
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var categories = await context.Categories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(s => new CategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .ToListAsync();

        var categoryIdsWithServices = await context.ServiceOffered
            .Select(s => s.CategoryId)
            .Distinct()
            .ToListAsync();

        var idSet = new HashSet<long>(categoryIdsWithServices);
        foreach (var category in categories)
        {
            category.HasAssociatedServices = idSet.Contains(category.Id);
        }

        return categories;
    }

    public async Task<CategoryDto?> GetByIdAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var category = await context.Categories
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new CategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .FirstOrDefaultAsync();

        if (category is not null)
        {
            category.HasAssociatedServices = await context.ServiceOffered.AnyAsync(srv => srv.CategoryId == id);
        }

        return category;
    }

    public async Task<CategoryUpdateDto?> GetForEditAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var category = await context.Categories
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new CategoryUpdateDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .FirstOrDefaultAsync();
            
        if (category is not null)
        {
            category.HasAssociatedServices = await context.ServiceOffered.AnyAsync(srv => srv.CategoryId == id);
        }

        return category;
    }

    public async Task<long> AddAsync(CategoryCreateDto dto)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var entity = new Category
        {
            Name = dto.Name,
            Description = dto.Description
        };
        context.Categories.Add(entity);
        await context.SaveChangesAsync();
        return entity.Id;
    }

    public async Task UpdateAsync(CategoryUpdateDto updated)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var existing = await context.Categories
            .FirstOrDefaultAsync(c => c.Id == updated.Id);
        if (existing == null)
        {
            throw new KeyNotFoundException($"Categoria com ID {updated.Id} não foi encontrada.");
        }

        var hasServices = await context.ServiceOffered.AnyAsync(s => s.CategoryId == updated.Id);
        if (!hasServices)
        {
            existing.Name = updated.Name;
        }
        existing.Description = updated.Description;

        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(long id)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var hasServices = await context.ServiceOffered.AnyAsync(s => s.CategoryId == id);
        if (hasServices)
        {
            throw new InvalidOperationException("Não é possível excluir uma categoria que possui serviços associados.");
        }
        
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<PaginatedResult<CategoryDto>> GetPagedAsync(FilterParams filter)
    {
        await using var context = await contextFactory.CreateDbContextAsync();
        var query = context.Categories.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(filter.Search))
        {
            var search = filter.Search.ToLower();
            query = query.Where(x =>
                x.Name.ToLower().Contains(search) ||
                (x.Description != null && x.Description.ToLower().Contains(search))
            );
        }

        var total = await query.CountAsync();
        
        query = query.OrderBy(x => x.Name);

        var categories = await query
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .Select(s => new CategoryDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description
            })
            .ToListAsync();
            
        var categoryIdsOnPage = categories.Select(c => c.Id).ToList();
        var categoryIdsWithServices = await context.ServiceOffered
            .Where(s => categoryIdsOnPage.Contains(s.CategoryId))
            .Select(s => s.CategoryId)
            .Distinct()
            .ToListAsync();

        var idSet = new HashSet<long>(categoryIdsWithServices);
        foreach (var category in categories)
        {
            category.HasAssociatedServices = idSet.Contains(category.Id);
        }

        return new PaginatedResult<CategoryDto>
        {
            Items = categories,
            TotalCount = total,
            PageNumber = filter.PageNumber,
            PageSize = filter.PageSize
        };
    }
}
