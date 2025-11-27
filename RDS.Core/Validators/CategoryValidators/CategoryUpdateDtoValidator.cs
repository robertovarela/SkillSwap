using FluentValidation;
using RDS.Core.Dtos.Category;

namespace RDS.Core.Validators.CategoryValidators;

public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
{
    public CategoryUpdateDtoValidator()
    {
        Include(new CategoryBaseDtoValidator());
    }
}