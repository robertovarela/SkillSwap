using FluentValidation;
using RDS.Core.Dtos.ApplicationUser;

namespace RDS.Core.Validators.UserRegisterValidators;

public class UserRegisterDtoValidator : AbstractValidator<RegisterUserDto>
{
    public UserRegisterDtoValidator()
    {
        RuleFor(x => x.UserName)
            .NotEmpty().WithMessage("UserName é obrigatório.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email é obrigatório.")
            .EmailAddress().WithMessage("Email inválido.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Senha é obrigatória.")
            .MinimumLength(6).WithMessage("Senha precisa ter ao menos 6 caracteres.");

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password).WithMessage("As senhas não coincidem.");

        RuleFor(x => x.IsProfessional)
            .NotNull();

        RuleFor(x => x.IsClient)
            .NotNull();
    }
}