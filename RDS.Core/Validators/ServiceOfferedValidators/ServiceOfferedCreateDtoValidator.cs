using FluentValidation;
using RDS.Core.Dtos.ServiceOffered;

namespace RDS.Core.Validators.ServiceOfferedValidators;

public class ServiceOfferedCreateDtoValidator : AbstractValidator<ServiceOfferedCreateDto>
{
    public ServiceOfferedCreateDtoValidator()
    {
        Include(new ServiceOfferedBaseDtoValidator());

        RuleFor(x => x.ProfessionalProfileId)
            .GreaterThan(0).WithMessage("ProfessionalProfileId inválido.");
    }
}