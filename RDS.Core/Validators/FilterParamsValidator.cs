using FluentValidation;
using RDS.Core.Requests;

namespace RDS.Core.Validators;

/// <summary>
/// Validação para filtros de consultas.
/// </summary>
public class FilterParamsValidator : AbstractValidator<FilterParams>
{
    public FilterParamsValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("O número da página deve ser maior que 0.");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("O tamanho da página deve ser entre 1 e 100.");

        RuleFor(x => x.OrderDir)
            .Must(x => x == "asc" || x == "desc")
            .When(x => !string.IsNullOrEmpty(x.OrderDir))
            .WithMessage("A direção da ordenação deve ser 'asc' ou 'desc'.");
    }
}