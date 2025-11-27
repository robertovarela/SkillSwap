using FluentValidation;
using RDS.Core.Dtos.SkillSwapRequest;

namespace RDS.Core.Validators.SkillSwapRequestValidators;

public class SkillSwapRequesCreateDtoValidator : AbstractValidator<SkillSwapRequestCreateDto>
{
    public SkillSwapRequesCreateDtoValidator()
    {
        RuleFor(x => x.ProfessionalBId)
            .GreaterThan(0).WithMessage("ProfessionalBId deve ser maior que zero.");

        RuleFor(x => x.ProfessionalAId)
            .Cascade(CascadeMode.Stop)
            .GreaterThan(0)
            .NotEqual(x => x.ProfessionalBId).WithMessage("Os profissionais devem ser diferentes.");

        RuleFor(x => x.ServiceBId)
            .GreaterThan(0).WithMessage("ServiceBId inválido.");

        // ServiceAId ou OfferedAmount deve estar preenchido (um dos dois)
        RuleFor(x => x)
            .Must(x => x.ServiceAId.HasValue || x.OfferedAmount.HasValue)
            .WithMessage("Você deve oferecer um serviço OU um valor em SkillDólares.");

        // Se ServiceAId estiver preenchido, deve ser maior que 0
        RuleFor(x => x.ServiceAId)
            .GreaterThan(0)
            .When(x => x.ServiceAId.HasValue)
            .WithMessage("O Serviço para Swap deve ser válido.");

        // Se OfferedAmount estiver preenchido, deve ser maior que 0
        RuleFor(x => x.OfferedAmount)
            .GreaterThan(0)
            .When(x => x.OfferedAmount.HasValue)
            .WithMessage("O valor oferecido em SkillDólares deve ser maior que zero.");

        RuleFor(x => x.SwapDate)
            .GreaterThan(DateTime.MinValue).WithMessage("SwapDate é obrigatório.");
    }
}