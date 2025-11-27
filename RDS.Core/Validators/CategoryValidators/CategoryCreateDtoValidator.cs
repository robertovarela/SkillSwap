using FluentValidation;
using RDS.Core.Dtos.Category;

namespace RDS.Core.Validators.CategoryValidators;

public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
{
    public CategoryCreateDtoValidator()
    {
        Include(new CategoryBaseDtoValidator());
    }
}