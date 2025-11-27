using FluentValidation;
using RDS.Core.Dtos.ServiceOffered;

namespace RDS.Core.Validators.ServiceOfferedValidators;

public class ServiceOfferedUpdateDtoValidator : AbstractValidator<ServiceOfferedUpdateDto>
{
    public ServiceOfferedUpdateDtoValidator()
    {
        Include(new ServiceOfferedBaseDtoValidator());
    }
}