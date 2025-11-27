using FluentValidation;
using RDS.Core.Dtos.ServiceOffered;

namespace RDS.Core.Validators.ServiceOfferedValidators;

public class ServiceOfferedBaseDtoValidator : AbstractValidator<ServiceOfferedBaseDto>
{
    public ServiceOfferedBaseDtoValidator()
    {
        RuleFor(x => x.Title)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O título do serviço é obrigatório.")
            .MinimumLength(8).WithMessage("O título deve ter no mínimo 8 caracteres")
            .MaximumLength(100).WithMessage("O título deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Description)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A descrição é obrigatória.")
            .MinimumLength(8).WithMessage("A descrição deve ter no mínimo 8 caracteres.")
            .MaximumLength(1000).WithMessage("A descrição deve ter no máximo 1000 caracteres.");

        RuleFor(x => x.Price)
            .Cascade(CascadeMode.Stop)
            .GreaterThanOrEqualTo(0).When(x => x.Price.HasValue)
            .WithMessage("O preço deve ser positivo.");

        RuleFor(x => x.CategoryId)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("A categoria é obrigatória.")
            .GreaterThan(0).WithMessage("A categoria é obrigatória.");
    }
}