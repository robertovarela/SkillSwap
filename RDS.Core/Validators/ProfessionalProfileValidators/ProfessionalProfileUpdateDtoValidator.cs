using FluentValidation;
using RDS.Core.Dtos.ProfessionalProfile;

namespace RDS.Core.Validators.ProfessionalProfileValidators;

public class ProfessionalProfileUpdateDtoValidator : AbstractValidator<ProfessionalProfileUpdateDto>
{
    public ProfessionalProfileUpdateDtoValidator()
    {
        Include(new ProfessionalProfileCreateDtoValidator());

        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id inválido.");

        RuleFor(x => x.Bio)
            .MaximumLength(1000).WithMessage("Bio não pode exceder 1000 caracteres.");

        RuleFor(x => x.Expertise)
            .NotEmpty().WithMessage("Expertise é obrigatória.")
            .MaximumLength(200).WithMessage("Expertise não pode exceder 200 caracteres.");
    }
}