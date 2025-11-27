using FluentValidation;
using RDS.Core.Dtos.Category;

namespace RDS.Core.Validators.CategoryValidators;

public class CategoryBaseDtoValidator : AbstractValidator<CategoryBaseDto>
{
    public CategoryBaseDtoValidator()
    {
        RuleFor(x => x.Name)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O nome da categoria é obrigatório.")
            .MinimumLength(3).WithMessage("O nome deve ter no mínimo 3 caracteres.")
            .MaximumLength(100).WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MinimumLength(3).WithMessage("A descrição deve ter no mínimo 3 caracteres.")
            .MaximumLength(200).WithMessage("A descrição deve ter no máximo 200 caracteres.");
    }
}