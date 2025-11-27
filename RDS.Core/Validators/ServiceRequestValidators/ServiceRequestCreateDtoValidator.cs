using FluentValidation;
using RDS.Core.Dtos.ServiceRequest;

namespace RDS.Core.Validators.ServiceRequestValidators;

public class ServiceRequestCreateDtoValidator : AbstractValidator<ServiceRequestCreateDto>
{
    public ServiceRequestCreateDtoValidator()
    {
        RuleFor(x => x.ClientId)
            .GreaterThan(0).WithMessage("Cliente inválido.");

        RuleFor(x => x.ServiceId)
            .GreaterThan(0).WithMessage("Serviço inválido.");

        RuleFor(x => x.Observations)
            .MaximumLength(1000).When(x => !string.IsNullOrEmpty(x.Observations))
            .WithMessage("Observações devem ter no máximo 1000 caracteres.");
    }
}