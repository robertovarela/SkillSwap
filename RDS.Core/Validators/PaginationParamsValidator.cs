using FluentValidation;
using RDS.Core.Requests;

namespace RDS.Core.Validators;

/// <summary>
/// Validação para parâmetros de paginação.
/// </summary>
public class PaginationParamsValidator : AbstractValidator<PaginationParams>
{
    public PaginationParamsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("O número da página deve ser maior que 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("O tamanho da página deve ser entre 1 e 100.");
    }
}