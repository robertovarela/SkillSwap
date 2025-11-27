using FluentValidation;
using RDS.Core.Dtos.ServiceOffered;

namespace RDS.Core.Validators.ServiceOfferedValidators;

public class ServiceOfferedDtoValidator : AbstractValidator<ServiceOfferedDto>
{
    public ServiceOfferedDtoValidator()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotEmpty().WithMessage("O serviço é obrigatório.")
            .GreaterThan(0).WithMessage("O serviço é obrigatório.");
    }
}