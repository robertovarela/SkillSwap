using RDS.Core.Dtos.Category;
using RDS.Core.Requests;
using RDS.Core.Utils;

namespace RDS.Core.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<CategoryDto>> GetAllAsync();
    Task<CategoryDto?> GetByIdAsync(long id);
    Task<CategoryUpdateDto?> GetForEditAsync(long id);
    Task<long> AddAsync(CategoryCreateDto category);
    Task UpdateAsync(CategoryUpdateDto category);
    Task DeleteAsync(long id);
    Task<PaginatedResult<CategoryDto>> GetPagedAsync(FilterParams filter);
}