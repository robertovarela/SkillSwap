using RDS.Core.Dtos.Category;
using RDS.Core.Interfaces;
using RDS.Core.Libraries.Services;
using RDS.Core.Models;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Infrastructure.Services;

/// <summary>
/// Implementação do serviço de categorias.
/// </summary>
public class CategoryService(ICategoryRepository repo) : ICategoryService
{
    public async Task<IEnumerable<CategoryDto>> GetAllAsync()
    {
        return await repo.GetAllAsync();
    }

    public async Task<CategoryDto?> GetByIdAsync(long id)
    {
        return await repo.GetByIdAsync(id);
    }

    public Task<CategoryUpdateDto?> GetForEditAsync(long id)
    {
        return repo.GetForEditAsync(id);
    }

    public async Task<long> AddAsync(CategoryCreateDto dto)
    {
        var entity = new Category
        {
            Name = dto.Name,
            Description = dto.Description,
        };
        await repo.AddAsync(dto);
        return entity.Id;
    }

    public async Task UpdateAsync(CategoryUpdateDto category)
    {
        await repo.UpdateAsync(category);
    }
    
    public async Task DeleteAsync(long id)
    {
        await repo.DeleteAsync(id);
    }

    public async Task<PaginatedResult<CategoryDto>> GetPagedAsync(FilterParams filter)
    {
        return await repo.GetPagedAsync(filter);
    }
}