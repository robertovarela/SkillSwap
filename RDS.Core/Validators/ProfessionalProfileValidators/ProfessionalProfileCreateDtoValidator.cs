using FluentValidation;
using RDS.Core.Dtos.ProfessionalProfile;
using RDS.Core.Models;

namespace RDS.Core.Validators.ProfessionalProfileValidators;

public class ProfessionalProfileCreateDtoValidator : AbstractValidator<ProfessionalProfileCreateDto>
{
    private const int MinimumAge = 18;
    
    public ProfessionalProfileCreateDtoValidator()
    {
        RuleFor(x => x.ProfessionalName)
            .NotEmpty().WithMessage("O nome profissional é obrigatório.")
            .Length(3, 100).WithMessage("O nome profissional deve ter entre 3 e 100 caracteres.");

        RuleFor(x => x.Bio)
            .NotEmpty().WithMessage("A biografia é obrigatória.")
            .Length(10, 1000).WithMessage("A biografia deve ter entre 10 e 1000 caracteres.");

        RuleFor(x => x.Expertise)
            .NotEmpty().WithMessage("A especialização é obrigatória.")
            .Length(10, 200).WithMessage("A especialização deve ter entre 10 e 200 caracteres.");

        RuleFor(x => x.AcademicRegistry)
            .NotEmpty().WithMessage("O Registro Acadêmico é obrigatório.")
            .MaximumLength(15).WithMessage("O Registro Acadêmico deve ter no máximo 15 caracteres.");

        RuleFor(x => x.TeachingInstitution)
            .NotEmpty().WithMessage("A Instituição de Ensino é obrigatória.")
            .MaximumLength(100).WithMessage("A Instituição de Ensino deve ter no máximo 100 caracteres.");

        RuleFor(x => x.Cpf)
            .NotEmpty().WithMessage("O CPF é obrigatório.")
            .Must(Cpf.IsValid).WithMessage("O CPF informado é inválido.");

        RuleFor(x => x.BirthDate)
            .NotEmpty().WithMessage("A data de nascimento é obrigatória.")
            .Must(BeAtLeastMinimumAge).WithMessage($"Você deve ter no mínimo {MinimumAge} anos para se cadastrar.");
    }

    private bool BeAtLeastMinimumAge(DateOnly birthDate)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var minimumAgeDate = today.AddYears(-MinimumAge);
        
        return birthDate <= minimumAgeDate;
    }
}
